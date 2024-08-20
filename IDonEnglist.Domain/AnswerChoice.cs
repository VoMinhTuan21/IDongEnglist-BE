using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class AnswerChoice : BaseDomainEntity
    {
        public string? Text { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public Answer? Answer { get; set; }
        public ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
