using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class QuestionGroup : BaseDomainEntity
    {
        public string? Instruction { get; set; }
        public string TextContent { get; set; }
        public int Order { get; set; }
        public int TestSectionId { get; set; }
        public TestSection TestSection { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<QuestionGroupMedia> QuestionGroupMedias { get; set; }
    }
}
