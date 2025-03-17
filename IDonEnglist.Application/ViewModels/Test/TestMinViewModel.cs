using IDonEnglist.Application.ViewModels.Common;

namespace IDonEnglist.Application.ViewModels.Test
{
    public class TestMinViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int FinalTestId { get; set; }
        public int TestTypeId { get; set; }
    }
}
