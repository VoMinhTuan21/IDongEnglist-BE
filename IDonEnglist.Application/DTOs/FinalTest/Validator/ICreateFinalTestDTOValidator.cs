using FluentValidation;

namespace IDonEnglist.Application.DTOs.FinalTest.Validator
{
    public class ICreateFinalTestDTOValidator : AbstractValidator<CreateFinalTestDTO>
    {
        public ICreateFinalTestDTOValidator()
        {
            Include(new IFinalTestDTOValidator());
        }
    }
}
