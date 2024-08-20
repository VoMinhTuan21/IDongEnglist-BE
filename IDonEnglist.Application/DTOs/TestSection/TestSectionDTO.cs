using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Passage;
using IDonEnglist.Application.DTOs.Test;
using IDonEnglist.Application.DTOs.TestPart;

namespace IDonEnglist.Application.DTOs.TestSection
{
    public class TestSectionDTO : BaseDTO
    {
        public int TestPartId { get; set; }
        public TestPartDTO TestPart { get; set; }
        public int TestId { get; set; }
        public TestDTO Test { get; set; }
        public string? Instruction { get; set; }
        public int? PassageId { get; set; }
        public PassageDTO? Passage { get; set; }
    }
}
