using AutoMapper;
using IDonEnglist.Application.DTOs.TestPart;
using IDonEnglist.Application.Features.TestParts.Commands;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.TestTypes.Events
{
    public class TestTypeUpdatedNotification : INotification
    {
        public int TestTypeId { get; set; }
        public List<UpdateTestPartDTO> UpdatedParts { get; set; }
        public List<TestPart> OldParts { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class UpdateTestPartsNotificationHandler : INotificationHandler<TestTypeUpdatedNotification>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateTestPartsNotificationHandler(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task Handle(TestTypeUpdatedNotification notification, CancellationToken cancellationToken)
        {
            // Need to do: Delete parts that are not in UpdatedParts
            var partsShouldUpdate = notification.UpdatedParts.Where(p => Int32.TryParse(p.Id, out _)).ToList();
            var partsShouldCreate = notification.UpdatedParts.Where(p => !Int32.TryParse(p.Id, out _)).ToList();
            var partsShouldDelete = notification.OldParts.Where(p => !notification.UpdatedParts.Any(up => up.Id == p.Id.ToString())).ToList();

            await _mediator.Send(new UpdateTestParts
            {
                CurrentUser = notification.CurrentUser,
                UpdateData = partsShouldUpdate
            }, cancellationToken);

            await _mediator.Send(new CreateTestParts
            {
                CreateData = _mapper.Map<List<CreateTestPartDTO>>(partsShouldCreate),
                CurrentUser = notification.CurrentUser,
                TestTypeId = notification.TestTypeId
            }, cancellationToken);

            await _mediator.Send(new DeleteTestParts
            {
                DeleteData = partsShouldDelete.Select(p => p.Id).ToList(),
                CurrentUser = notification.CurrentUser
            }, cancellationToken);
        }
    }
}
