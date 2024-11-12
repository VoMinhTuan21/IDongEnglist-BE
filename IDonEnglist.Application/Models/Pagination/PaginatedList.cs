namespace IDonEnglist.Application.Models.Pagination
{
    public class PaginatedList<T> where T : class
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public PaginatedList(List<T> items, int pageNumber, int pageSize, int totalRecords)
        {
            this.Items = items;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.TotalRecords = totalRecords;
            this.TotalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize);
        }

        public PaginatedList()
        {
            this.Items = new List<T>();
            this.PageNumber = 0;
            this.PageSize = 0;
            this.TotalRecords = 0;
        }
    }
}
