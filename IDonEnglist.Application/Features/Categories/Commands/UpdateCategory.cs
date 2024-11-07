using AutoMapper;
using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.DTOs.Category.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Features.Categories.Events;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.Category;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.Categories.Commands
{
    public class UpdateCategory : IRequest<CategoryViewModel>
    {
        public UpdateCategoryDTO UpdateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategory, CategoryViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CategoryViewModel> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await ValidateRequest(request);

                var categoryOriginal = await _unitOfWork.CategoryRepository.GetByIdAsync(request.UpdateData.Id)
                    ?? throw new NotFoundException(nameof(Category), request.UpdateData.Id);

                var temp = _mapper.Map(request.UpdateData, categoryOriginal);

                if (request.UpdateData.Name != null)
                {
                    temp.Code = Utils.SlugGenerator.GenerateSlug(request.UpdateData.Name);
                }

                await _unitOfWork.CategoryRepository.UpdateAsync(temp, request.CurrentUser);
                await _unitOfWork.Save();

                if (request.UpdateData.Skills != null)
                {
                    await _mediator.Publish(new CategoryUpdatedNotification
                    {
                        Skills = request.UpdateData.Skills,
                        CategoryId = categoryOriginal.Id,
                        CurrentUser = request.CurrentUser,
                    });
                }


                var category = await _unitOfWork.CategoryRepository
                    .GetOneAsync(c => c.Id == request.UpdateData.Id, false,
                        query => query.Include(c => c.Skills.Where(sk => sk.DeletedBy == null && sk.DeletedDate == null)).AsNoTracking()
                    );

                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<CategoryViewModel>(category);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private async Task ValidateRequest(UpdateCategory request)
        {
            var validator = new UpdateCategoryDTOValidator(_unitOfWork.CategoryRepository);
            var validationResult = await validator.ValidateAsync(request.UpdateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            await CheckForDuplicateName(request);
            await CheckForDuplicateCode(request);
        }
        private async Task CheckForDuplicateName(UpdateCategory request)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.GetOneAsync(c => c.Name == request.UpdateData.Name);
            if (existingCategory != null && existingCategory.Id != request.UpdateData.Id)
            {
                throw new BadRequestException("The name has been used.");
            }
        }
        private async Task CheckForDuplicateCode(UpdateCategory request)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.GetOneAsync(c => c.Code == request.UpdateData.Code);
            if (existingCategory != null && existingCategory.Id != request.UpdateData.Id)
            {
                throw new BadRequestException("The code has been used.");
            }
        }
    }
}
