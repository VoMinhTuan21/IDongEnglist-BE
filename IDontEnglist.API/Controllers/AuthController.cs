using IDonEnglist.Application.DTOs.User;
using IDonEnglist.Application.ViewModels.User;
using IDonEnglist.Identity.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IDonEnglist.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult<RegisterUserViewModel>> SignUp([FromBody] SignUpUserDTO signUpData)
        {
            var user = await _authService.SignUp(signUpData);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<RegisterUserViewModel>> Login([FromBody] LoginUserDTO loginData)
        {
            var user = await _authService.Login(loginData);

            return Ok(user);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<RefreshTokenViewModel>> Refresh([FromBody] RefreshTokenDTO refreshData)
        {
            var newToken = await _authService.Refresh(refreshData);

            return Ok(newToken);
        }
    }
}
// hello