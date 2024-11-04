using IDonEnglist.Application.DTOs.TestPart;

namespace IDonEnglist.Application.DTOs.TestType
{
    public class CreateTestTypeDTO : ITestTypeDTO
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Questions { get; set; }
        public int CategorySkillId { get; set; }
        public List<CreateTestPartDTO> Parts { get; set; }
    }
}
