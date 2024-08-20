using IDonEnglist.Application.DTOs.Collection;
using IDonEnglist.Application.DTOs.Common;

namespace IDonEnglist.Application.DTOs.FinalTest
{
    public class FinalTestDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int CollectionId { get; set; }
        public CollectionDTO Collection { get; set; }
    }
}
