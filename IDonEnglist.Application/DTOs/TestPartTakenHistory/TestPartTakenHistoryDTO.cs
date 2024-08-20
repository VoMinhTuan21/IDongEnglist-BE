using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.TestPart;
using IDonEnglist.Application.DTOs.TestTakenHistory;
using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.DTOs.TestPartTakenHistory
{
    public class TestPartTakenHistoryDTO : BaseDTO
    {
        public int TestPartId { get; set; }
        public TestPartDTO TestPart { get; set; }
        public int TestTakenHistoryId { get; set; }
        public TestTakenHistoryDTO TestTakenHistory { get; set; }
        public TestTakenStatus Status { get; set; }
        public float Score { get; set; }
        public int Duration { get; set; }
    }
}
