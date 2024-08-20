using FluentValidation;
using IDonEnglist.Application.Persistence.Contracts;

namespace IDonEnglist.Application.DTOs.Category.Validators
{
    public class UpdateCategoryDTOValidator : AbstractValidator<UpdateCategoryDTO>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryDTOValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            Include(new ICategoryDTOValidator(_categoryRepository));

            RuleFor(p => p.Id)
                .NotNull().NotEmpty().WithMessage("{Property} must be presented")
                .GreaterThan(0).WithMessage("{Property} must be greater than 0");
        }
    }
}
