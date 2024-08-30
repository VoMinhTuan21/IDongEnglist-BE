using IDonEnglist.Application.DTOs.User;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly IDonEnglistDBContext _dbContext;
        public UserRepository(IDonEnglistDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<bool> ExistsAsync(CheckUserExistDTO checkUserExistDTO)
        {
            var existingUsers = await _dbContext.Users
                .Where(u => u.Name == checkUserExistDTO.Name ||
                            (!string.IsNullOrEmpty(checkUserExistDTO.Email) && u.Email == checkUserExistDTO.Email) ||
                            (!string.IsNullOrEmpty(checkUserExistDTO.Phone) && u.Phone == checkUserExistDTO.Phone))
                .ToListAsync();

            var errorMessages = new List<string>();

            if (existingUsers.Any(u => u.Name == checkUserExistDTO.Name))
            {
                errorMessages.Add("This name has already been used.");
            }

            if (!string.IsNullOrEmpty(checkUserExistDTO.Email) && existingUsers.Any(u => u.Email == checkUserExistDTO.Email))
            {
                errorMessages.Add("This email has already been used.");
            }

            if (!string.IsNullOrEmpty(checkUserExistDTO.Phone) && existingUsers.Any(u => u.Phone == checkUserExistDTO.Phone))
            {
                errorMessages.Add("This phone has already been used.");
            }

            if (errorMessages.Any())
            {
                throw new BadRequestException(string.Join(" ", errorMessages));
            }

            return false;
        }
    }
}
