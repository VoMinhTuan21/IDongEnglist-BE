using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.FinalTest;
using IDonEnglist.Application.DTOs.Media;

namespace IDonEnglist.Application.DTOs.Test
{
    public class TestDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? AudioId { get; set; }
        public MediaDTO? Audio { get; set; }
        public int TestTaken { get; set; }
        public int FinalTestId { get; set; }
        public FinalTestDTO FinalTest { get; set; }
    }
}
