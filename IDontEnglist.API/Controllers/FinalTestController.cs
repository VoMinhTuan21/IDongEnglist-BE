using IDonEnglist.Application.DTOs.FinalTest;
using IDonEnglist.Application.Features.FinalTests.Commands;
using IDonEnglist.Application.Features.FinalTests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IDonEnglist.API.Controllers
{
    [Route("api/final-test")]
    [ApiController]
    [Authorize]
    public class FinalTestController : BaseController
    {
        private readonly IMediator _mediator;

        public FinalTestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateFinalTestDTO createData)
        {
            var command = new CreateFinalTest
            {
                CreateData = createData,
                CurrentUser = GetUserFromToken()
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        // This API return PaginatedList<FinalTestViewModelMin> or PaginatedList<FinalTestViewModel>
        public async Task<ActionResult<object>> GetPagination([FromQuery] GetPaginationFinalTestsDTO filter)
        {
            var query = new GetPaginationFinalTests
            {
                FilterData = filter
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update([FromBody] UpdateFinalTestDTO updateData)
        {
            var command = new UpdateFinalTest
            {
                UpdateData = updateData,
                CurrentUser = GetUserFromToken()
            };

            var result = await _mediator.Send<int>(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete([FromRoute] int id)
        {
            var command = new DeleteFinalTest
            {
                DeleteData = new Application.DTOs.Common.BaseDTO
                {
                    Id = id
                },
                CurrentUser = GetUserFromToken()
            };

            var result = await _mediator.Send<int>(command);

            return Ok(result);
        }
    }
}
