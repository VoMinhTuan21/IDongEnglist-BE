using FluentValidation;
using IDonEnglist.Application.DTOs.Common.Validator;

namespace IDonEnglist.Application.DTOs.Test.Validators
{
    public class CreateTestDTOValidator : AbstractValidator<CreateTestDTO>
    {
        public CreateTestDTOValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.FinalTestId)
                .NotNull().NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
            RuleFor(p => p.CategorySkillId)
                .NotNull().NotEmpty().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");

            When(p => p.Audio != null, () =>
            {
                RuleFor(p => p.Audio).SetValidator(new FileDTOValidator());
            });
        }
    }
}
