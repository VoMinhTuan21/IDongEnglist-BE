using FluentValidation;

namespace IDonEnglist.Application.DTOs.FinalTest.Validator
{
    public class IFinalTestDTOValidator : AbstractValidator<IFinalTestDTO>
    {
        public IFinalTestDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.CollectionId)
                .NotEmpty().NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
        }
    }
}
