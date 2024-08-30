using FluentValidation;

namespace IDonEnglist.Application.DTOs.Role.Validators
{
    public class UpdateRoleDTOValidator : AbstractValidator<UpdateRoleDTO>
    {
        public UpdateRoleDTOValidator()
        {
            Include(new IRoleDTOValidator());

            RuleFor(p => p.Id)
                .NotNull().NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must greater than 0");
        }
    }
}
