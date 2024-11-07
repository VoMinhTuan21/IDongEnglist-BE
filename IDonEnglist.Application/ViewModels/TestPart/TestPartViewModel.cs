using IDonEnglist.Application.ViewModels.Common;

namespace IDonEnglist.Application.ViewModels.TestPart
{
    public class TestPartViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }
        public int Questions { get; set; }
        public int TestTypeId { get; set; }
        public int Order { get; set; }
    }
}
