using FluentValidation;

namespace IDonEnglist.Application.DTOs.TestType.validators
{
    public class GetTestTypeDetailDTOValidator : AbstractValidator<GetTestTypeDetailDTO>
    {
        public GetTestTypeDetailDTOValidator()
        {
            RuleFor(p => p.CategorySkillId)
                .NotEmpty().NotEmpty().WithMessage("{Property} is required")
                .GreaterThan(0).WithMessage("{Property} must greater then {ComparisonValue}");
        }
    }
}
