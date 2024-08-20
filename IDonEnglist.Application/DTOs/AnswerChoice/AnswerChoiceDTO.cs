using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Question;

namespace IDonEnglist.Application.DTOs.AnswerChoice
{
    public class AnswerChoiceDTO : BaseDTO
    {
        public string? Text { get; set; }
        public int QuestionId { get; set; }
        public QuestionDTO Question { get; set; }
    }
}
