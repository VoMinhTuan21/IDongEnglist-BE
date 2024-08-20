using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class Question : BaseDomainEntity
    {
        public string? Text { get; set; }
        public QuestionType Type { get; set; }
        public int GroupId { get; set; }
        public QuestionGroup Group { get; set; }

        public ICollection<AnswerChoice> Choices { get; set; }
        public Answer Answer { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
