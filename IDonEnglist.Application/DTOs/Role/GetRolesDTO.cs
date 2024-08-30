using IDonEnglist.Application.DTOs.Pagination;

namespace IDonEnglist.Application.DTOs.Role
{
    public class GetRolesDTO : PaginationDTO
    {
        public string? KeyWord { get; set; }
    }
}
