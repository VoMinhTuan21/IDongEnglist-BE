using IDonEnglist.Application.DTOs.Test;
using IDonEnglist.Application.Features.Tests.Commands;
using IDonEnglist.Application.ViewModels.Test;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IDonEnglist.API.Controllers
{
    [Route("api/test")]
    [ApiController]
    [Authorize]
    public class TestController : BaseController
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<TestMinViewModel>> Create([FromBody] CreateTestDTO createData)
        {
            var command = new CreateTest
            {
                CreateData = createData,
                CurrentUser = GetUserFromToken()
            };

            var result = await _mediator.Send<TestMinViewModel>(command);

            return Ok(result);
        }
    }
}
