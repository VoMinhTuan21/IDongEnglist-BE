using IDonEnglist.Application.DTOs.Collection;
using IDonEnglist.Application.Features.Collections.Commands;
using IDonEnglist.Application.Features.Collections.Queries;
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
        // This function return type is PaginatedList<CollectionViewModel> or PaginatedList<CollectionViewModelMin>
        public async Task<ActionResult<object>> GetPagination([FromQuery] GetPaginationCollectionsDTO filter)
        {
            var query = new GetPaginationCollections { Filter = filter };
            var result = await _mediator.Send(query);

            return Ok(result);
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

        [HttpPut]
        public async Task<ActionResult<int>> Update([FromBody] UpdateCollectionDTO updateData)
        {
            var currentUser = GetUserFromToken();

            var collectionId = await _mediator.Send(
                new UpdateCollection
                {
                    CurrentUser = currentUser,
                    UpdateDataFromController = updateData
                }
            );

            return Ok(collectionId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var currentUser = GetUserFromToken();

            var collectionId = await _mediator.Send(
                new DeleteCollection
                {
                    CurrentUser = currentUser,
                    DeleteData = new Application.DTOs.Common.BaseDTO { Id = id }
                }
            );

            return Ok(collectionId);
        }
    }
}
