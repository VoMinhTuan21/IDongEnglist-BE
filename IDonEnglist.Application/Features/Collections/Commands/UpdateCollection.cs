using AutoMapper;
using IDonEnglist.Application.DTOs.Collection;
using IDonEnglist.Application.DTOs.Collection.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Features.Collections.Events;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Collections.Commands
{
    public class UpdateCollection : IRequest<int>
    {
        public UpdateCollectionDTO? UpdateDataFromController { get; set; }
        public UpdateCollectionFromCommandDTO? UpdateDataFromCommand { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class UpdateCollectionHandler : IRequestHandler<UpdateCollection, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateCollectionHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<int> Handle(UpdateCollection request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await ValidateRequest(request);

                if (request.UpdateDataFromCommand is not null)
                {
                    var collection = await _unitOfWork.CollectionRepository.GetByIdAsync(request.UpdateDataFromCommand.Id)
                        ?? throw new NotFoundException(nameof(Collection), request.UpdateDataFromCommand.Id);

                    collection.ThumbnailId = request.UpdateDataFromCommand.ThumbnailId;

                    await _unitOfWork.CollectionRepository.UpdateAsync(collection, request.CurrentUser);
                    await _unitOfWork.Save();

                    return collection.Id;
                }

                if (request.UpdateDataFromController is not null)
                {
                    var collection = await _unitOfWork.CollectionRepository.GetByIdAsync(request.UpdateDataFromController.Id);

                    if (request.UpdateDataFromController.Thumbnail is not null)
                    {
                        await _mediator.Publish(new UpdatedCollectionNotification
                        {
                            CurrentUser = request.CurrentUser,
                            ThumbnailId = collection?.ThumbnailId ?? 0,
                            Thumbnail = request.UpdateDataFromController.Thumbnail
                        });
                    }

                    var updatedCollection = _mapper.Map(request.UpdateDataFromController, collection);
                    updatedCollection.Code = SlugGenerator.GenerateSlug(request.UpdateDataFromController.Name ?? "");

                    await _unitOfWork.CollectionRepository.UpdateAsync(updatedCollection, request.CurrentUser);
                    await _unitOfWork.Save();

                    await _unitOfWork.CommitTransactionAsync();

                    return updatedCollection.Id;
                }

                return 0;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private async Task ValidateRequest(UpdateCollection request)
        {
            if (request.UpdateDataFromCommand is not null)
            {
                var validator = new UpdateCollectionFromCommandDTOValidator();
                var validationResult = await validator.ValidateAsync(request.UpdateDataFromCommand);

                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult);
                }
            }

            if (request.UpdateDataFromController is not null)
            {
                var validator = new UpdateCollectionDTOValidator();
                var validationResult = await validator.ValidateAsync(request.UpdateDataFromController);

                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult);
                }

                var exist = await _unitOfWork.CollectionRepository.ExistsAsync(request.UpdateDataFromController.Id);

                if (!exist)
                {
                    throw new NotFoundException(nameof(Collection), request.UpdateDataFromController.Id);
                }

                var existCode = await _unitOfWork.CollectionRepository
                    .GetOneAsync(cl => cl.Code == SlugGenerator.GenerateSlug(request.UpdateDataFromController.Name ?? ""));

                if (existCode != null && existCode.Id != request.UpdateDataFromController.Id)
                {
                    throw new BadRequestException("Please use another name");
                }
            }
        }
    }
}
