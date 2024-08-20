using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class Answer : BaseDomainEntity
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int? AnswerChoiceId { get; set; }
        public AnswerChoice? AnswerChoice { get; set; }
        public string? SampleText { get; set; }
        public int? SampleAudioId { get; set; }
        public Media? SampleAudio { get; set; }
    }
}
