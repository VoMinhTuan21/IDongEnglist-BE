using IDonEnglist.API.Attributes;
using IDonEnglist.Application.Constants;
using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.Features.Categories.Commands;
using IDonEnglist.Application.Features.Categories.Queries;
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
        [HasPermission(PermissionTypes.CreateCategory)]
        public async Task<ActionResult<IReadOnlyList<CategoryDTO>>> Get()
        {
            var categories = await _mediator.Send(new GetCategories());

            return Ok(categories);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _mediator.Send(new GetCategory() { Id = id });

            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post([FromBody] CreateCategoryDTO dto)
        {
            var currentUser = GetUserFromToken();

            var command = new CreateCategory { CreateData = dto, CurrentUser = currentUser };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> Put(int id, [FromBody] UpdateCategoryDTO dto)
        {
            var currentUser = GetUserFromToken();

            var command = new UpdateCategory { UpdateData = dto, CurrentUser = currentUser };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var currentUser = GetUserFromToken();

            var command = new DeleteCategory { Id = id, CurrentUser = currentUser };
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
