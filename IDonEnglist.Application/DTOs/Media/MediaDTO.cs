using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.DTOs.Media
{
    public class MediaDTO : BaseDTO
    {
        public MediaType Type { get; set; }
        public string Url { get; set; }
        public MediaContextType ContextType { get; set; }
        public string? Transcript { get; set; }
    }
}
