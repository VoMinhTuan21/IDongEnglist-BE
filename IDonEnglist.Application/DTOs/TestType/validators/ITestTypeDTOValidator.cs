using FluentValidation;

namespace IDonEnglist.Application.DTOs.TestType.validators
{
    public class ITestTypeDTOValidator : AbstractValidator<ITestTypeDTO>
    {
        public ITestTypeDTOValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("{PropertyName} is required");
            RuleFor(p => p.Questions)
                 .NotEmpty().NotNull().WithMessage("{PropertyName} is required")
                 .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
            RuleFor(p => p.Duration)
                 .NotEmpty().NotNull().WithMessage("{PropertyName} is required")
                 .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
            RuleFor(p => p.CategorySkillId)
                 .NotEmpty().NotNull().WithMessage("{PropertyName} is required")
                 .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
        }
    }
}
