using FluentValidation;

namespace IDonEnglist.Application.DTOs.User.Validators
{
    public class RefreshTokenDTOValidator : AbstractValidator<RefreshTokenDTO>
    {
        public RefreshTokenDTOValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required.")
                .Must(token =>
                {
                    return Guid.TryParse(token, out _);
                }).WithMessage("{Property} not valid.");
            RuleFor(x => x.Token).NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
