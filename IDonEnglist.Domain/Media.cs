using IDonEnglist.Domain.Common;

namespace IDonEnglist.Domain
{
    public class Media : BaseDomainEntity
    {
        public MediaType Type { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public MediaContextType ContextType { get; set; }
        public string? Transcript { get; set; }

        public Collection? Collection { get; set; }
        public Test? Test { get; set; }
        public Answer? Answer { get; set; }
        public QuestionGroupMedia? QuestionGroupMedia { get; set; }
        public UserAnswer? UserAnswer { get; set; }
    }
}
