using IDonEnglist.Application.DTOs.Collection;
using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Media;
using IDonEnglist.Application.Features.Collections.Commands;
using IDonEnglist.Application.Features.Medias.Commands;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Domain.Common;
using MediatR;

namespace IDonEnglist.Application.Features.Collections.Events
{
    public class CreatedCollectionNotification : INotification
    {
        public FileDTO Thumbnail { get; set; }
        public int CollectionId { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class CreateThumbnailForCollectionHandler : INotificationHandler<CreatedCollectionNotification>
    {
        private readonly IMediator _mediator;

        public CreateThumbnailForCollectionHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Handle(CreatedCollectionNotification notification, CancellationToken cancellationToken)
        {
            var thumbnail = await _mediator.Send(new CreateMedia
            {
                CreateData = new CreateMediaDTO
                {
                    ContextType = MediaContextType.Other,
                    PublicId = notification.Thumbnail.PublicId,
                    Type = MediaType.Image,
                    Url = notification.Thumbnail.Url,
                },
                CurrentUser = notification.CurrentUser,
            }, cancellationToken);

            await _mediator.Send(new UpdateCollection
            {
                CurrentUser = notification.CurrentUser,
                UpdateDataFromCommand = new UpdateCollectionFromCommandDTO
                {
                    ThumbnailId = thumbnail.Id,
                    Id = notification.CollectionId,
                }
            }, cancellationToken);
        }
    }
}
