using AutoMapper;
using IDonEnglist.Application.Constants;
using IDonEnglist.Application.DTOs.User;
using IDonEnglist.Application.DTOs.User.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.User;
using IDonEnglist.Domain;
using IDonEnglist.Identity.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IDonEnglist.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthService(IOptions<JWTSettings> jwtSettings, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private JwtSecurityToken GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(CustomClaimTypes.Id, user.Id.ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //var hmac = new System.Security.Cryptography.HMACSHA256()
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // The Key property provides a randomly generated salt.
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public async Task<RegisterUserViewModel> SignUp(SignUpUserDTO signUpData)
        {
            var validator = new SignUpUserDTOValidator();
            var validationResult = await validator.ValidateAsync(signUpData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var existed = await _unitOfWork.UserRepository.ExistsAsync(_mapper.Map<CheckUserExistDTO>(signUpData));

            if (existed)
            {
                throw new BadRequestException("Any of information you provided has been used.");
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(signUpData.Password, out passwordHash, out passwordSalt);

            var newUser = _mapper.Map<User>(signUpData);
            newUser.Password = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            var userRole = await _unitOfWork.RoleRepository.GetOneAsync(r => r.Code == "client");

            if (userRole != null)
            {
                newUser.RoleId = userRole.Id;
            }

            var user = await _unitOfWork.UserRepository.AddAsync(newUser);

            await _unitOfWork.Save();

            return _mapper.Map<RegisterUserViewModel>(user);
        }

        public async Task<LoginUserViewModel> Login(LoginUserDTO loginData)
        {
            var validator = new LoginUserDTOValidator();
            var validationResult = await validator.ValidateAsync(loginData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var user = await _unitOfWork.UserRepository.GetOneAsync(u => u.Email == loginData.Email)
                ?? throw new NotFoundException(nameof(User), loginData);

            if (!VerifyPasswordHash(loginData.Password, user.Password, user.PasswordSalt))
            {
                throw new BadRequestException("Password did not match");
            }

            JwtSecurityToken token = GenerateToken(user);

            var response = _mapper.Map<LoginUserViewModel>(user);
            var refreshToken = Guid.NewGuid().ToString();

            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.RefreshToken = refreshToken;

            user.RefreshToken = refreshToken;

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.Save();

            return response;
        }

        public async Task<RefreshTokenViewModel> Refresh(RefreshTokenDTO refreshData)
        {
            var principal = GetPrincipalFromExpiredToken(refreshData.Token);
            var userIdClaim = principal.FindFirst(CustomClaimTypes.Id)
                ?? throw new SecurityTokenException("User ID not found in access token.");

            var user = await _unitOfWork.UserRepository.GetByIdAsync(int.Parse(userIdClaim.Value ?? "0"));

            if (user == null || user.RefreshToken != refreshData.RefreshToken)
            {
                throw new SecurityTokenException("Invalid refresh token.");
            }

            var newAccessToken = GenerateToken(user);
            var newRefreshToken = Guid.NewGuid().ToString();

            user.RefreshToken = newRefreshToken;
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.Save();

            var newToken = new RefreshTokenViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };

            return newToken;
        }
    }
}
