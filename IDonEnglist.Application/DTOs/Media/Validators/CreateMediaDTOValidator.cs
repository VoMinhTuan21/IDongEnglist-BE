using FluentValidation;

namespace IDonEnglist.Application.DTOs.Media.Validators
{
    public class CreateMediaDTOValidator : AbstractValidator<CreateMediaDTO>
    {
        public CreateMediaDTOValidator()
        {
            RuleFor(p => p.PublicId)
                .NotNull().NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(p => p.Url)
                .NotNull().NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(p => p.Type)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required")
                .IsInEnum().WithMessage("{PropertyName} is invalid");
            RuleFor(p => p.ContextType)
                .NotNull().NotEmpty().WithMessage("{PropertyName} is required")
                .IsInEnum().WithMessage("{PropertyName} is invalid");
        }
    }
}
