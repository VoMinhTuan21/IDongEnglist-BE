using AutoMapper;
using IDonEnglist.Application.DTOs.Collection;
using IDonEnglist.Application.DTOs.Collection.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Features.Collections.Events;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.Collection;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.Collections.Commands
{
    public class CreateCollection : IRequest<CollectionViewModel>
    {
        public CreateCollectionDTO CreateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class CreateCollectionHandler : IRequestHandler<CreateCollection, CollectionViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateCollectionHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<CollectionViewModel> Handle(CreateCollection request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await ValidateRequest(request);

                var temp = _mapper.Map<Collection>(request.CreateData);

                temp.Code ??= SlugGenerator.GenerateSlug(temp.Name);

                await _unitOfWork.CollectionRepository.AddAsync(temp, request.CurrentUser);

                await _unitOfWork.Save();

                await _mediator.Publish(new CreatedCollectionNotification
                {
                    CollectionId = temp.Id,
                    CurrentUser = request.CurrentUser,
                    Thumbnail = request.CreateData.Thumbnail,
                }, cancellationToken);

                var collection = await _unitOfWork.CollectionRepository
                    .GetByIdAsync(temp.Id, query => query.Include(c => c.Thumbnail));

                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<CollectionViewModel>(collection);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        private async Task ValidateRequest(CreateCollection request)
        {
            var validator = new CreateCollectionDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            await CheckExistCategory(request.CreateData.CategoryId);
            await CheckExistCollection(request);
        }
        private async Task CheckExistCategory(int categoryId)
        {
            var existed = await _unitOfWork.CategoryRepository.GetOneAsync(c => c.Id == categoryId && c.ParentId != null)
                ?? throw new NotFoundException(nameof(Category), categoryId);
        }
        private async Task CheckExistCollection(CreateCollection request)
        {
            var existed = await _unitOfWork.CollectionRepository
                .GetOneAsync(c => c.Name == request.CreateData.Name && c.CategoryId == request.CreateData.CategoryId);

            if (existed is not null)
            {
                throw new BadRequestException("This collection already exists.");
            }
        }
    }
}
