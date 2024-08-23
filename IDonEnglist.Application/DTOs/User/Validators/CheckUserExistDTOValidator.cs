using FluentValidation;

namespace IDonEnglist.Application.DTOs.User.Validators
{
    public class CheckUserExistDTOValidator : AbstractValidator<CheckUserExistDTO>
    {
        public CheckUserExistDTOValidator()
        {
            Include(new IUserDTOValidator());
        }
    }
}
