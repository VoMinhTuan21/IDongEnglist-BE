using IDonEnglist.Application.DTOs.Pagination;

namespace IDonEnglist.Application.DTOs.FinalTest
{
    public class GetPaginationFinalTestsDTO : PaginationDTO
    {
        public int? CollectionId { get; set; }
        public string? Keywords { get; set; }
    }
}
