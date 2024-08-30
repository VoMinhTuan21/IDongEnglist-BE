namespace IDonEnglist.Application.DTOs.Pagination
{
    public class PaginationDTO
    {
        public string SortBy { get; set; } = "Id";
        public bool Ascending { get; set; } = true;
        public bool WithDeleted { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
