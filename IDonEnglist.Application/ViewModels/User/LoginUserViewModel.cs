namespace IDonEnglist.Application.ViewModels.User
{
    public class LoginUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
