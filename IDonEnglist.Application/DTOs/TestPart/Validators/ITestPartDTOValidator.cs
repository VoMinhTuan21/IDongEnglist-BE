using FluentValidation;

namespace IDonEnglist.Application.DTOs.TestPart.Validators
{
    public class ITestPartDTOValidator : AbstractValidator<ITestPartDTO>
    {
        public ITestPartDTOValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Order)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
            RuleFor(p => p.Duration)
               .NotEmpty().NotNull().WithMessage("{PropertyName} is required")
               .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
            RuleFor(p => p.Questions)
               .NotEmpty().NotNull().WithMessage("{PropertyName} is required")
               .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
        }
    }
}
