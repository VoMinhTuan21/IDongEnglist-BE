using FluentValidation;

namespace IDonEnglist.Application.DTOs.Permission.Validators
{
    public class CreatePermissionDTOValidator : AbstractValidator<CreatePermissionDTO>
    {
        public CreatePermissionDTOValidator()
        {
            Include(new IPermissionDTOValidator());

        }
    }
}
