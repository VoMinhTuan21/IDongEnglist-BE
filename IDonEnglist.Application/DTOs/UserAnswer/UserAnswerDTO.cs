using IDonEnglist.Application.DTOs.AnswerChoice;
using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Media;
using IDonEnglist.Application.DTOs.Question;
using IDonEnglist.Application.DTOs.TestSectionTakenHistory;

namespace IDonEnglist.Application.DTOs.UserAnswer
{
    public class UserAnswerDTO : BaseDTO
    {
        public int TestSectionTakenId { get; set; }
        public TestSectionTakenHistoryDTO TestSectionTakenHistory { get; set; }
        public int? AnswerChoiceId { get; set; }
        public AnswerChoiceDTO? AnswerChoice { get; set; }
        public string? AnswerText { get; set; }
        public int? AnswerAudioId { get; set; }
        public MediaDTO? AnswerAudio { get; set; }
        public int QuestionId { get; set; }
        public QuestionDTO Question { get; set; }
    }
}
