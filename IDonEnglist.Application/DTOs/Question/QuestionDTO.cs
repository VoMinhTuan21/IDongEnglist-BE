using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.QuestionGroup;
using IDonEnglist.Domain.Common;

namespace IDonEnglist.Application.DTOs.Question
{
    public class QuestionDTO : BaseDTO
    {
        public string? Text { get; set; }
        public QuestionType Type { get; set; }
        public int GroupId { get; set; }
        public QuestionGroupDTO Group { get; set; }
    }
}
