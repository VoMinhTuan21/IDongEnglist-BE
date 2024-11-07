using IDonEnglist.Application.Features.CategorySkills.Commands;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Domain.Common;
using MediatR;

namespace IDonEnglist.Application.Features.Categories.Events
{
    public class CategoryCreatedNotification : INotification
    {
        public int CategoryId { get; set; }
        public IList<Skill> Skills { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class AddSkillsCategoryCreatedNotification : INotificationHandler<CategoryCreatedNotification>
    {
        private readonly IMediator _mediator;

        public AddSkillsCategoryCreatedNotification(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(CategoryCreatedNotification notification, CancellationToken cancellationToken)
        {
            for (int i = 0; i < notification.Skills.Count; i++)
            {
                await _mediator.Send(new CreateCategorySkill
                {
                    CreateData = new DTOs.CategorySkill.CreateCategorySkillDTO
                    {
                        CategoryId = notification.CategoryId,
                        Skill = notification.Skills[i]
                    },
                    CurrentUser = notification.CurrentUser
                });
            }
        }
    }
}
