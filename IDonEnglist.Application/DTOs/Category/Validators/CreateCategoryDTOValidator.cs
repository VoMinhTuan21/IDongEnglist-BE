using FluentValidation;
using IDonEnglist.Application.Persistence.Contracts;

namespace IDonEnglist.Application.DTOs.Category.Validators
{
    public class CreateCategoryDTOValidator : AbstractValidator<CreateCategoryDTO>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryDTOValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

            Include(new ICategoryDTOValidator(_categoryRepository));
        }
    }
}
