using FluentValidation;

namespace IDonEnglist.Application.DTOs.CategorySkill.Validator
{
    public class UpdateCategorySkillDTOValidator : AbstractValidator<UpdateCategorySkillDTO>
    {
        public UpdateCategorySkillDTOValidator()
        {
            RuleFor(p => p.CategoryId)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleForEach(p => p.Skills).IsInEnum().WithMessage("{PropertyName} is not valid.");
            RuleFor(p => p.Skills)
                .NotNull().NotEmpty().WithMessage("{PropertyName} is required");

        }
    }
}
