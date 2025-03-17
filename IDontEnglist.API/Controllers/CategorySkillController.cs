using IDonEnglist.Application.DTOs.CategorySkill;
using IDonEnglist.Application.Features.CategorySkills.Commands;
using IDonEnglist.Application.Features.CategorySkills.Queries;
using IDonEnglist.Application.ViewModels.CategorySkill;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IDonEnglist.API.Controllers
{
    [Route("api/category-skill")]
    [ApiController]
    [Authorize]
    public class CategorySkillController : BaseController
    {
        private readonly IMediator _mediator;

        public CategorySkillController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CategorySkillViewModel>> Post([FromBody] CreateCategorySkillDTO createData)
        {
            var currentUser = GetUserFromToken();
            var categorySkill = await _mediator.Send(new CreateCategorySkill
            {
                CreateData = createData,
                CurrentUser = currentUser
            });

            return Ok(categorySkill);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var currentUser = GetUserFromToken();
            var deletedId = await _mediator.Send(new DeleteCategorySkill { Id = id, CurrentUser = currentUser });

            return Ok(new { Id = deletedId });
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<CategorySkillMiniViewModel>>> GetList([FromQuery] GetListCategorySkillsDTO filter)
        {
            var query = new GetListCategorySkills
            {
                Fitler = filter
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategorySkillViewModel>> Get(int id)
        {
            var result = await _mediator.Send(new GetCategorySkillDetail { Id = id });
            return Ok(result);
        }
    }
}
