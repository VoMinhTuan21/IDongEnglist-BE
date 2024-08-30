namespace IDonEnglist.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<HashSet<string>> GetPermissionsAsync(int UserId);
    }
}
