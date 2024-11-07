using IDonEnglist.Application.ViewModels.Common;
using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.ViewModels.Media
{
    public class MediaViewModel : BaseViewModel
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
        public MediaType Type { get; set; }
        public MediaContextType ContextType { get; set; }
        public string? Transcript { get; set; }
    }
}
