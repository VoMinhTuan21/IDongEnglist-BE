using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Media;

namespace IDonEnglist.Application.DTOs.QuestionGroupMedia
{
    public class QuestionGroupMediaDTO : BaseDTO
    {
        public int MediaId { get; set; }
        public MediaDTO Media { get; set; }
        public int QuestionGroupId { get; set; }
    }
}
