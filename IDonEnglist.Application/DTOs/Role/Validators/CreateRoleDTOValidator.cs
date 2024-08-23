using FluentValidation;

namespace IDonEnglist.Application.DTOs.Role.Validators
{
    public class CreateRoleDTOValidator : AbstractValidator<CreateRoleDTO>
    {
        public CreateRoleDTOValidator()
        {
            Include(new IRoleDTOValidator());
        }
    }
}
