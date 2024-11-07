using IDonEnglist.Application.DTOs.CategorySkill;
using IDonEnglist.Application.Features.CategorySkills.Commands;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Domain.Common;
using MediatR;

namespace IDonEnglist.Application.Features.Categories.Events
{
    public class CategoryUpdatedNotification : INotification
    {
        public int CategoryId { get; set; }
        public IList<Skill> Skills { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class UpdateSkillsCategoryCreatedNotification : INotificationHandler<CategoryUpdatedNotification>
    {
        private readonly IMediator _mediator;

        public UpdateSkillsCategoryCreatedNotification(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Handle(CategoryUpdatedNotification notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateCategorySkill
            {
                CurrentUser = notification.CurrentUser,
                UpdateData = new UpdateCategorySkillDTO { CategoryId = notification.CategoryId, Skills = notification.Skills }
            }, cancellationToken);
        }
    }
}
