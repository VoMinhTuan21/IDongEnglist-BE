using IDonEnglist.Application.DTOs.Pagination;

namespace IDonEnglist.Application.DTOs.Collection
{
    public class GetPaginationCollectionsDTO : PaginationDTO
    {
        public int? CategoryId { get; set; }
        public string? Keywords { get; set; }
    }
}
