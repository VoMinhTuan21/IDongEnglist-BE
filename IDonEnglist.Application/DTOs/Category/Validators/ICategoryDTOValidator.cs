using FluentValidation;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Persistence.Contracts;

namespace IDonEnglist.Application.DTOs.Category.Validators
{
    public class ICategoryDTOValidator : AbstractValidator<ICategoryDTO>
    {
        private readonly ICategoryRepository _categoryRepository;

        public ICategoryDTOValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.").When(dto => !IsUpdate(dto))
                .NotNull().WithMessage("{PropertyName} is required.").When(dto => !IsUpdate(dto))
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters");

            RuleFor(p => p.Code)
                .MaximumLength(255).When(p => !string.IsNullOrEmpty(p.Code)).WithMessage("{PropertyName} must not exceed 255 characters");

            RuleFor(p => p.ParentId)
                .GreaterThan(0)
            .MustAsync(async (id, token) =>
            {
                var category = await _categoryRepository.ExistsAsync(id ?? 0);

                if (!category)
                {
                    throw new NotFoundException(nameof(Category), id ?? 0);
                }

                return true;
            }).When(p => p.ParentId.HasValue).WithMessage("{PropertyName} must be a valid category Id");

            RuleForEach(p => p.Skills).IsInEnum().When(p => p.Skills != null && p.Skills.Count > 0).WithMessage("{PropertyName} is not valid");
        }
        private bool IsUpdate(ICategoryDTO dto)
        {
            if (dto.GetType().GetProperty("Id") != null)
            {
                return true;
            }

            return false;
        }
    }
}
