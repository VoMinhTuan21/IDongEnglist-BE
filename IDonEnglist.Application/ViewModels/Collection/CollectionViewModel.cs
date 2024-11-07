using IDonEnglist.Application.ViewModels.Common;

namespace IDonEnglist.Application.ViewModels.Collection
{
    public class CollectionViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Thumbnail { get; set; }
        public int CategoryId { get; set; }
    }
}
