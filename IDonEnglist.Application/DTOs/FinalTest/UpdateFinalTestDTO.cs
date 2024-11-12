using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.FinalTest
{
    public class UpdateFinalTestDTO : BaseDTO, IFinalTestDTO
    {
        public string Name { get; set; }
        public int CollectionId { get; set; }
    }
}
