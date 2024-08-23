using IDonEnglist.Application.DTOs.Role;
using IDonEnglist.Application.Features.Roles.Commands;
using IDonEnglist.Application.ViewModels.Role;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace IDonEnglist.API.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<RoleViewModel>> Post([FromBody] CreateRoleDTO createData)
        {
            var role = await _mediator.Send(new CreateRole { createData = createData });

            return Ok(role);
        }
    }
}
