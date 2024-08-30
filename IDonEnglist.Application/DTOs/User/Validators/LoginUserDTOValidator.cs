using FluentValidation;

namespace IDonEnglist.Application.DTOs.User.Validators
{
    public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
    {
        public LoginUserDTOValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("Please enter a valid email address");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(8).WithMessage("{PropertyName} must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("{PropertyName} must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("{PropertyName} must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("{PropertyName} must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("{PropertyName} must contain at least one special character.");
        }
    }
}
