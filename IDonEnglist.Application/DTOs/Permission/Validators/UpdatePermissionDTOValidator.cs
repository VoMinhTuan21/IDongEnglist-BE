using FluentValidation;

namespace IDonEnglist.Application.DTOs.Permission.Validators
{
    public class UpdatePermissionDTOValidator : AbstractValidator<UpdatePermissionDTO>
    {
        public UpdatePermissionDTOValidator()
        {
            Include(new IPermissionDTOValidator());

            RuleFor(p => p.Id)
                .NotNull().NotEmpty()
                .WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{Property} must greater than 0.");
        }
    }
}
