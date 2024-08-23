using FluentValidation;

namespace IDonEnglist.Application.DTOs.User.Validators
{
    public class IUserDTOValidator : AbstractValidator<IUserDTO>
    {
        public IUserDTOValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("{PropertyName} is required");
            RuleFor(p => p.Email).EmailAddress().When(p => string.IsNullOrEmpty(p.Email)).WithMessage("{PropertyName} is not a valid email");
        }
    }
}
