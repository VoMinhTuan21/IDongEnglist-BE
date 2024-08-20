using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.TestPartTakenHistory;
using IDonEnglist.Application.DTOs.TestSection;
using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.DTOs.TestSectionTakenHistory
{
    public class TestSectionTakenHistoryDTO : BaseDTO
    {
        public int TestPartTakenHistoryId { get; set; }
        public TestPartTakenHistoryDTO TestPartTakenHistory { get; set; }
        public int TestSectionId { get; set; }
        public TestSectionDTO TestSection { get; set; }
        public TestTakenStatus Status { get; set; }
    }
}
