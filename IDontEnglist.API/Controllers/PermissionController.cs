using IDonEnglist.Application.Features.Permissions.Queries;
using IDonEnglist.Application.Models.Pagination;
using IDonEnglist.Application.ViewModels.Permission;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IDonEnglist.API.Controllers
{
    [Route("api/permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<PermissionViewModel>>> Get()
        {
            var permissions = await _mediator.Send(new GetPermissions());

            return Ok(permissions);
        }
    }
}
