using IDonEnglist.Application.DTOs.TestSection;

namespace IDonEnglist.Application.DTOs.QuestionGroup
{
    public class QuestionGroupDTO
    {
        public string? Instruction { get; set; }
        public string TextContent { get; set; }
        public int Order { get; set; }
        public int TestSectionId { get; set; }
        public TestSectionDTO TestSection { get; set; }
    }
}
