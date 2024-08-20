using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class QuestionGroupMedia : BaseDomainEntity
    {
        public int MediaId { get; set; }
        public Media Media { get; set; }
        public int QuestionGroupId { get; set; }
        public QuestionGroup QuestionGroup { get; set; }
    }
}
