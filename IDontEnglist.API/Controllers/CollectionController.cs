using IDonEnglist.Application.DTOs.Collection;
using IDonEnglist.Application.Features.Collections.Commands;
using IDonEnglist.Application.ViewModels.Collection;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IDonEnglist.API.Controllers
{
    [Route("api/collection")]
    [ApiController]
    [Authorize]
    public class CollectionController : BaseController
    {
        private readonly IMediator _mediator;

        public CollectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<ActionResult<CollectionViewModel>> Post([FromBody] CreateCollectionDTO createData)
        {
            var currentUser = GetUserFromToken();
            var collection = await _mediator.Send(
                new CreateCollection
                {
                    CreateData = createData,
                    CurrentUser = currentUser
                }
            );

            return Ok(collection);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
