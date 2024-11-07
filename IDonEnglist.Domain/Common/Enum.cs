namespace IDonEnglist.Domain.Common
{
    public enum Skill
    {
        Listening = 1,
        Reading = 5,
        Writing = 10,
        Speaking = 15
    }

    public enum QuestionType
    {
        SingleChoice = 1,
        Speaking = 4,
        Writing = 8
    }

    public enum MediaType
    {
        Audio = 1,
        Image = 2,
    }

    public enum MediaContextType
    {
        Test = 1,
        Answer = 3,
        Other = 5
    }

    public enum TestTakenStatus
    {
        Completed = 1,
        InProgress = 6
    }
}
