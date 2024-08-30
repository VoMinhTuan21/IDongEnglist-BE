using FluentValidation;

namespace IDonEnglist.Application.DTOs.Permission.Validators
{
    public class IPermissionDTOValidator : AbstractValidator<IPermissionDTO>
    {
        public IPermissionDTOValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().NotNull()
                .WithMessage("{PropertyName} is required")
                .When(dto => !IsUpdate(dto));
            RuleFor(a => a.Code)
               .Matches(@"^[a-z0-9]+(?:-[a-z0-9]+)*$")
               .When(a => !string.IsNullOrEmpty(a.Code))
               .WithMessage("{PropertyName} must be lowercase letters, numbers, and hyphens only.");
        }

        private bool IsUpdate(IPermissionDTO dto)
        {
            if (dto.GetType().GetProperty("Id") != null)
            {
                return true;
            }
            return false;
        }
    }
}
