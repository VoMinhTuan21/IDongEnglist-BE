using Microsoft.AspNetCore.Http;

namespace IDonEnglist.Application.DTOs.Media
{
    public class UploadImageDTO
    {
        public IFormFile Image { get; set; }
    }
}
