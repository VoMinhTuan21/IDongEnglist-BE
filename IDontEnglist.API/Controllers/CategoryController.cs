using IDonEnglist.API.Attributes;
using IDonEnglist.Application.Constants;
using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.Features.Categories.Commands;
using IDonEnglist.Application.Features.Categories.Queries;
using IDonEnglist.Application.ViewModels.Category;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IDonEnglist.API.Controllers
{
    [Route("api/category")]
    [ApiController]
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryViewModel>>> Get()
        {
            var categories = await _mediator.Send(new GetCategories());

            return Ok(categories);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        [HasPermission(PermissionTypes.ReadCategory)]
        public async Task<ActionResult<CategoryDetailViewModel>> Get(int id)
        {
            var category = await _mediator.Send(new GetCategory() { Id = id });

            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        [HasPermission(PermissionTypes.CreateCategory)]
        public async Task<ActionResult<CategoryViewModel>> Post([FromBody] CreateCategoryDTO dto)
        {
            var currentUser = GetUserFromToken();

            var command = new CreateCategory { CreateData = dto, CurrentUser = currentUser };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        // PUT api/<CategoryController>
        [HttpPut]
        [HasPermission(PermissionTypes.UpdateCategory)]
        public async Task<ActionResult<CategoryViewModel>> Put([FromBody] UpdateCategoryDTO dto)
        {
            var currentUser = GetUserFromToken();

            var command = new UpdateCategory { UpdateData = dto, CurrentUser = currentUser };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        [HasPermission(PermissionTypes.DeleteCategory)]
        public async Task<ActionResult<CategoryViewModel>> Delete(int id)
        {
            var currentUser = GetUserFromToken();

            var command = new DeleteCategory { Id = id, CurrentUser = currentUser };
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
