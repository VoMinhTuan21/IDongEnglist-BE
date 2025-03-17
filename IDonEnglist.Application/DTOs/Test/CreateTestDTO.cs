using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.Test
{
    public class CreateTestDTO
    {
        public string Name { get; set; }
        public int CategorySkillId { get; set; }
        public int FinalTestId { get; set; }
        public FileDTO? Audio { get; set; }
    }
}
