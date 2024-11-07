using FluentValidation;

namespace IDonEnglist.Application.DTOs.Common.Validator
{
    public class BaseDTOValidator : AbstractValidator<BaseDTO>
    {
        public BaseDTOValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is requred")
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
        }
    }
}
