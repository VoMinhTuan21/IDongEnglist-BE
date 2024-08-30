using IDonEnglist.Application.DTOs.Role;
using IDonEnglist.Application.Features.Roles.Commands;
using IDonEnglist.Application.Features.Roles.Queries;
using IDonEnglist.Application.Models.Pagination;
using IDonEnglist.Application.ViewModels.Role;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace IDonEnglist.API.Controllers
{
    [Route("api/role")]
    [ApiController]
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<RoleViewModel>> Post([FromBody] CreateRoleDTO createData)
        {
            var currentUser = GetUserFromToken();

            var role = await _mediator.Send(new CreateRole { CreateData = createData, CurrentUser = currentUser });

            return Ok(role);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<RoleViewModel>>> GetAll(
            [FromQuery] string? keyword = null, [FromQuery] string? sortBy = null,
            [FromQuery] bool ascending = true, [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10, [FromQuery] bool widthDeleted = false)
        {
            var filter = new GetRolesDTO();
            filter.KeyWord = keyword;
            filter.PageNumber = pageNumber;
            filter.PageSize = pageSize;
            filter.Ascending = ascending;
            if (sortBy != null)
            {
                filter.SortBy = sortBy;
            }
            filter.WithDeleted = widthDeleted;


            var pagedList = await _mediator.Send(new GetRoles { FilterData = filter });

            return Ok(pagedList);
        }

        [HttpGet("id")]
        public async Task<ActionResult<RoleViewModel>> GetById(int id, [FromQuery] bool withDeleted = false)
        {
            var role = await _mediator.Send(new GetRole { Id = id, WithDeleted = withDeleted });

            return Ok(role);
        }

        [HttpPut]
        public async Task<ActionResult<RoleViewModel>> Update([FromBody] UpdateRoleDTO updateData)
        {
            var currentUser = GetUserFromToken();

            var role = await _mediator.Send(new UpdateRole { UpdateData = updateData, CurrentUser = currentUser });

            return Ok(role);
        }

        [HttpDelete("id")]
        public async Task<ActionResult<RoleViewModel>> Delete(int Id)
        {
            var currentUser = GetUserFromToken();

            var role = await _mediator.Send(new DeleteRole { Id = Id, CurrentUser = currentUser });

            return Ok(role);
        }
    }
}
