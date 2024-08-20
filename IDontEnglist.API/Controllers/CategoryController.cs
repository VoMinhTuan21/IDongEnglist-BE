using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.Features.Categories.Commands;
using IDonEnglist.Application.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IDonEnglist.API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<CategoryController>
        [HttpGet]
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
            var command = new CreateCategory { createData = dto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> Put(int id, [FromBody] UpdateCategoryDTO dto)
        {
            var command = new UpdateCategory { updateData = dto };
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var command = new DeleteCategory { Id = id };
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
