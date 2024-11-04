using FluentValidation;

namespace IDonEnglist.Application.DTOs.TestPart.Validators
{
    public class UpdateTestPartDTOValidator : AbstractValidator<UpdateTestPartDTO>
    {
        public UpdateTestPartDTOValidator()
        {
            Include(new ITestPartDTOValidator());
            RuleFor(p => p.Id).NotEmpty().NotNull().WithMessage("{PropertyName} must be required.");
        }
    }
}
