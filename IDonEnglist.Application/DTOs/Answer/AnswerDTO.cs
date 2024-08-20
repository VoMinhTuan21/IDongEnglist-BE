using IDonEnglist.Application.DTOs.AnswerChoice;
using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Media;
using IDonEnglist.Application.DTOs.Question;

namespace IDonEnglist.Application.DTOs.Answer
{
    public class AnswerDTO : BaseDTO
    {
        public int QuestionId { get; set; }
        public QuestionDTO Question { get; set; }
        public int? AnswerChoiceId { get; set; }
        public AnswerChoiceDTO? AnswerChoice { get; set; }
        public string? SampleText { get; set; }
        public int? SampleAudioId { get; set; }
        public MediaDTO? SampleAudio { get; set; }
    }
}
