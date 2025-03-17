using FluentValidation;

namespace IDonEnglist.Application.DTOs.CategorySkill.Validator
{
    public class GetListCategorySkillsDTOValidator : AbstractValidator<GetListCategorySkillsDTO>
    {
        public GetListCategorySkillsDTOValidator()
        {
            RuleFor(p => p.FinalTestId)
                .GreaterThan(0).When(p => p.FinalTestId != null).WithMessage("{PropertyName} must greater than {ComparisonValue}");
            RuleFor(p => p.CollectionId)
                .GreaterThan(0).When(p => p.CollectionId != null).WithMessage("{PropertyName} must greater than {ComparisonValue}");
        }
    }
}
