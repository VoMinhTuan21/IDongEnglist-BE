using IDonEnglist.Application.DTOs.User;
using IDonEnglist.Domain;

namespace IDonEnglist.Application.Persistence.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> ExistsAsync(CheckUserExistDTO checkUserExistDTO);
    }
}
