using FluentValidation;
using IDonEnglist.Application.DTOs.Common.Validator;

namespace IDonEnglist.Application.DTOs.FinalTest.Validator
{
    public class UpdateFinalTestDTOValidator : AbstractValidator<UpdateFinalTestDTO>
    {
        public UpdateFinalTestDTOValidator()
        {
            Include(new IFinalTestDTOValidator());

            Include(new BaseDTOValidator());
        }
    }
}
