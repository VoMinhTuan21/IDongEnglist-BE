using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Media;
using IDonEnglist.Application.DTOs.Test;
using IDonEnglist.Application.Features.Medias.Commands;
using IDonEnglist.Application.Features.Tests.Commands;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Domain.Common;
using MediatR;

namespace IDonEnglist.Application.Features.Tests.Events
{
    public class TestCreatedNotification : INotification
    {
        public int TestId { get; set; }
        public FileDTO Audio { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class AddAudioForTestHandler : INotificationHandler<TestCreatedNotification>
    {
        private readonly IMediator _mediator;

        public AddAudioForTestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Handle(TestCreatedNotification notification, CancellationToken cancellationToken)
        {
            var audio = await _mediator.Send(new CreateMedia
            {
                CreateData = new CreateMediaDTO
                {
                    ContextType = MediaContextType.Test,
                    PublicId = notification.Audio.PublicId,
                    Type = MediaType.Audio,
                    Url = notification.Audio.Url,
                },
                CurrentUser = notification.CurrentUser,
            }, cancellationToken);

            await _mediator.Send(new UpdateTest
            {
                CurrentUser = notification.CurrentUser,
                UpdateDataFromCommand = new UpdateTestFromCommandDTO
                {
                    Id = notification.TestId,
                    AudioId = audio.Id,
                }
            }, cancellationToken);
        }
    }
}
