using FluentValidation;

namespace IDonEnglist.Application.DTOs.Test.Validators
{
    public class UpdateTestFromCommandDTOValidator : AbstractValidator<UpdateTestFromCommandDTO>
    {
        public UpdateTestFromCommandDTOValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
            RuleFor(p => p.AudioId)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
        }
    }
}
