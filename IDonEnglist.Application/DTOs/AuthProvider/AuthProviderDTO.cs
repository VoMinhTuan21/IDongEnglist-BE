using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.AuthProvider
{
    public class AuthProviderDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? ApiKey { get; set; }
        public string? ApiSecret { get; set; }
        public string? RedirectUri { get; set; }
    }
}
