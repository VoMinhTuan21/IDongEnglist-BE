using FluentValidation;

namespace IDonEnglist.Application.DTOs.FinalTest.Validator
{
    public class IFinalTestDTOValidator : AbstractValidator<IFinalTestDTO>
    {
        public IFinalTestDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().NotEmpty().WithMessage("{PropertyName} is required.").When(dto => !IsUpdate(dto));
            RuleFor(p => p.CollectionId)
                .NotEmpty().NotEmpty().WithMessage("{PropertyName} is required.").When(dto => !IsUpdate(dto))
                .GreaterThan(0).WithMessage("{PropertyName} is required.");
        }

        private bool IsUpdate(IFinalTestDTO dto)
        {
            if (dto.GetType().GetProperty("Id") != null)
            {
                return true;
            }
            return false;
        }
    }
}
