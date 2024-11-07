using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.TestPart;

namespace IDonEnglist.Application.DTOs.TestType
{
    public class UpdateTestTypeDTO : BaseDTO, ITestTypeDTO
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int Questions { get; set; }
        public int CategorySkillId { get; set; }
        public List<UpdateTestPartDTO> Parts { get; set; }
    }
}
