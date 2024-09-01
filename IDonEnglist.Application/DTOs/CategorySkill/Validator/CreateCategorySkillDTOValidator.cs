using FluentValidation;

namespace IDonEnglist.Application.DTOs.CategorySkill.Validator
{
    public class CreateCategorySkillDTOValidator : AbstractValidator<CreateCategorySkillDTO>
    {
        public CreateCategorySkillDTOValidator()
        {
            RuleFor(p => p.CategoryId)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must greater than 0.");

            RuleFor(p => p.Skill)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required.")
                .IsInEnum().WithMessage("{PropertyName} value is not valid.");
        }
    }
}
