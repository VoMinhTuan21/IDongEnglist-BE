namespace IDonEnglist.Application.Models.Pagination
{
    public class PaginatedList<T> where T : class
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PaginatedList(List<T> items, int pageIndex, int pageSize, int totalRecords)
        {
            this.Items = items;
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalRecords = totalRecords;
            this.TotalPages = (int)Math.Ceiling((decimal)totalRecords / (decimal)pageSize);
        }

        public PaginatedList()
        {
            this.Items = new List<T>();
            this.PageIndex = 0;
            this.PageSize = 0;
            this.TotalRecords = 0;
        }
    }
}
