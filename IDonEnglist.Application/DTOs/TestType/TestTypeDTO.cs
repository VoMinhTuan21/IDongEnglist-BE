using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.TestType
{
    public class TestTypeDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfParts { get; set; }
        public int Duration { get; set; }
    }
}
