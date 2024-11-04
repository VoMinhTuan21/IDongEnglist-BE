using Microsoft.AspNetCore.Http;

namespace IDonEnglist.Application.DTOs.Media
{
    public class UploadAudioDTO
    {
        public IFormFile Audio { get; set; }
    }
}
