using AutoMapper;
using IDonEnglist.Application.DTOs.Collection;
using IDonEnglist.Application.DTOs.Collection.Validator;
using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.Collection;
using IDonEnglist.Domain;
using IDonEnglist.Domain.Common;
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

        public CreateCollectionHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CollectionViewModel> Handle(CreateCollection request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            var mediaId = await CreateMedia(request.CreateData.Thumbnail, request.CurrentUser);

            return await CreateNewCollection(request, mediaId);
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
            var existed = await _unitOfWork.CategoryRepository.ExistsAsync(categoryId);

            if (!existed)
            {
                throw new NotFoundException(nameof(Category), categoryId);
            }
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
        private async Task<int> CreateMedia(FileDTO thumbnail, CurrentUser currentUser)
        {
            var media = await _unitOfWork.MediaRepository.AddAsync(new Media
            {
                PublicId = thumbnail.PublicId,
                Url = thumbnail.Url,
                CreatedBy = currentUser.Id,
                Type = MediaType.Image,
                ContextType = MediaContextType.Other
            }, currentUser);

            await _unitOfWork.Save();

            return media.Id;
        }
        private async Task<CollectionViewModel> CreateNewCollection(CreateCollection request, int ThumbnailId)
        {
            var temp = _mapper.Map<Collection>(request.CreateData);
            temp.ThumbnailId = ThumbnailId;

            temp.Code ??= SlugGenerator.GenerateSlug(temp.Name);

            await _unitOfWork.CollectionRepository.AddAsync(temp, request.CurrentUser);

            await _unitOfWork.Save();

            var collection = await _unitOfWork.CollectionRepository
                .GetByIdAsync(temp.Id, query => query.Include(c => c.Thumbnail));

            return _mapper.Map<CollectionViewModel>(collection);
        }
    }
}
