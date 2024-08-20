using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.TestType;

namespace IDonEnglist.Application.DTOs.TestPart
{
    public class TestPartDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }
        public int NumberOfQuestions { get; set; }
        public int Order { get; set; }
        public int TestTypeId { get; set; }
        public TestTypeDTO TestType { get; set; }
    }
}
