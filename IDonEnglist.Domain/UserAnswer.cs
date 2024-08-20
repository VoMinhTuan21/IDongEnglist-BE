using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class UserAnswer : BaseDomainEntity
    {
        public int TestSectionTakenHistoryId { get; set; }
        public TestSectionTakenHistory TestSectionTakenHistory { get; set; }
        public int? AnswerChoiceId { get; set; }
        public AnswerChoice? AnswerChoice { get; set; }
        public string? AnswerText { get; set; }
        public int? AnswerAudioId { get; set; }
        public Media? AnswerAudio { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
