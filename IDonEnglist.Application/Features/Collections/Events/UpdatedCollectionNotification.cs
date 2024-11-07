using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Media.Validators;
using IDonEnglist.Application.Features.Medias.Commands;
using IDonEnglist.Application.Models.Identity;
using MediatR;

namespace IDonEnglist.Application.Features.Collections.Events
{
    public class UpdatedCollectionNotification : INotification
    {
        public FileDTO Thumbnail { get; set; }
        public int ThumbnailId { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class UpdateThumbnailForCollection : INotificationHandler<UpdatedCollectionNotification>
    {
        private readonly IMediator _mediator;

        public UpdateThumbnailForCollection(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Handle(UpdatedCollectionNotification notification, CancellationToken cancellationToken)
        {
            var command = new UpdateMedia
            {
                CurrentUser = notification.CurrentUser,
                UpdateData = new UpdateMediaDTO
                {
                    Id = notification.ThumbnailId,
                    PublicId = notification.Thumbnail.PublicId,
                    Url = notification.Thumbnail.Url,
                }
            };
            await _mediator.Send(command);
        }
    }
}
