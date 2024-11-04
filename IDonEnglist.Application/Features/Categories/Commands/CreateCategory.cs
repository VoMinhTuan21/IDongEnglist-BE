using AutoMapper;
using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.DTOs.Category.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Features.Categories.Events;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.Category;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Categories.Commands
{
    public class CreateCategory : IRequest<CategoryViewModel>
    {
        public CreateCategoryDTO CreateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class CreateCategoryHandler : IRequestHandler<CreateCategory, CategoryViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<CategoryViewModel> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            // Start a new transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Validate the request
                await ValidateRequest(request);

                // Set default code if not provided
                SetDefaultCodeIfEmpty(request);

                // Check for duplicate name or code
                await CheckForDuplicateNameOrCode(request);

                // Create the category entity and map it
                var categoryEntity = _mapper.Map<Category>(request.CreateData);

                // Add the category to the repository
                var category = await _unitOfWork.CategoryRepository.AddAsync(categoryEntity, request.CurrentUser);
                await _unitOfWork.Save();

                // If there are associated skills, handle them via the MediatR event
                if (request.CreateData.Skills != null && request.CreateData.Skills.Count > 0)
                {
                    await _mediator.Publish(new CategoryCreatedNotification
                    {
                        CategoryId = category.Id,
                        CurrentUser = request.CurrentUser,
                        Skills = request.CreateData.Skills
                    }, cancellationToken);
                }

                // Commit the transaction if everything was successful
                await _unitOfWork.CommitTransactionAsync();

                // Return the result mapped to the CategoryViewModel
                return _mapper.Map<CategoryViewModel>(category);
            }
            catch (Exception)
            {
                // Rollback transaction if anything goes wrong
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        private async Task ValidateRequest(CreateCategory request)
        {
            var validator = new CreateCategoryDTOValidator(_unitOfWork.CategoryRepository);
            var validationResult = await validator.ValidateAsync(request.CreateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }
        }

        private void SetDefaultCodeIfEmpty(CreateCategory request)
        {
            if (string.IsNullOrEmpty(request.CreateData.Code))
            {
                request.CreateData.Code = SlugGenerator.GenerateSlug(request.CreateData.Name);
            }
        }

        private async Task CheckForDuplicateNameOrCode(CreateCategory request)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.GetOneAsync(
                c => c.Name == request.CreateData.Name || c.Code == request.CreateData.Code);

            if (existingCategory != null)
            {
                throw new BadRequestException("Name or Code has been used.");
            }
        }
    }
}
