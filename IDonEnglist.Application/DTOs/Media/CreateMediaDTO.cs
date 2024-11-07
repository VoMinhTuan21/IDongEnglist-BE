using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.DTOs.Media
{
    public class CreateMediaDTO
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
        public MediaType Type { get; set; }
        public MediaContextType ContextType { get; set; }
        public string? Transcript { get; set; }
    }
}
