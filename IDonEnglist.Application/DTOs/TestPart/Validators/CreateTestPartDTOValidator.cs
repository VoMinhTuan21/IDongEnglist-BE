using FluentValidation;

namespace IDonEnglist.Application.DTOs.TestPart.Validators
{
    public class CreateTestPartDTOValidator : AbstractValidator<CreateTestPartDTO>
    {
        public CreateTestPartDTOValidator()
        {
            Include(new ITestPartDTOValidator());
        }
    }
}
