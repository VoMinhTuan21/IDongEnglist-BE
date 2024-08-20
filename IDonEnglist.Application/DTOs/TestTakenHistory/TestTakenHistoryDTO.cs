using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Test;
using IDonEnglist.Application.DTOs.User;
using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.DTOs.TestTakenHistory
{
    public class TestTakenHistoryDTO : BaseDTO
    {
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public int TestId { get; set; }
        public TestDTO Test { get; set; }
        public float Score { get; set; }
        public int Duration { get; set; }
        public TestTakenStatus Status { get; set; }
    }
}
