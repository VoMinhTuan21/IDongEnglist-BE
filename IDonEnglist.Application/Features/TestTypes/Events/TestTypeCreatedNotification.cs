using IDonEnglist.Application.DTOs.TestPart;
using IDonEnglist.Application.Features.TestParts.Commands;
using IDonEnglist.Application.Models.Identity;
using MediatR;

namespace IDonEnglist.Application.Features.TestTypes.Events
{
    public class TestTypeCreatedNotification : INotification
    {
        public int TestTypeId { get; set; }
        public CurrentUser CurrentUser { get; set; }
        public List<CreateTestPartDTO> CreateData { get; set; }
    }

    public class AddTestPartsTestTypeCreatedNotification : INotificationHandler<TestTypeCreatedNotification>
    {
        private readonly IMediator _mediator;
        public AddTestPartsTestTypeCreatedNotification(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Handle(TestTypeCreatedNotification notification, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateTestParts
            {
                CreateData = notification.CreateData,
                CurrentUser = notification.CurrentUser,
                TestTypeId = notification.TestTypeId
            }, cancellationToken);
        }
    }
}
