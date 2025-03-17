using IDonEnglist.Application.DTOs.Pagination;
using IDonEnglist.Application.DTOs.TestType;
using IDonEnglist.Application.Features.TestTypes.Commands;
using IDonEnglist.Application.Features.TestTypes.Queries;
using IDonEnglist.Application.Models.Pagination;
using IDonEnglist.Application.ViewModels.TestType;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IDonEnglist.API.Controllers
{
    [Route("api/test-type")]
    [ApiController]
    [Authorize]
    public class TestTypeController : BaseController
    {
        private readonly IMediator _mediator;
        public TestTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<TestTypeDetailViewModel>> Post([FromBody] CreateTestTypeDTO createData)
        {
            var currentUser = GetUserFromToken();
            var command = new CreateTestType { CreateData = createData, CurrentUser = currentUser };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<TestTypeItemListViewModel>>> GetPaginationTable([FromQuery] PaginationDTO filter)
        {
            var query = new GetPaginationTestTypes { Filter = filter };
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("detail")]
        public async Task<ActionResult<TestTypeDetailViewModel>> GetDetail([FromQuery] GetTestTypeDetailDTO filter)
        {
            var query = new GetTestTypeDetail
            {
                GetData = filter,
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<TestTypeDetailViewModel>> Update(UpdateTestTypeDTO updateData)
        {
            var currentUser = GetUserFromToken();
            var command = new UpdateTestType { UpdateData = updateData, CurrentUser = currentUser };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete([FromRoute] int id)
        {
            var command = new DeleteTestType { CurrentUser = GetUserFromToken(), TestTypeId = id };

            await _mediator.Send(command);

            return Ok(id);
        }
    }
}
