using FluentValidation;

namespace IDonEnglist.Application.DTOs.Collection.Validator
{
    public class CreateCollectionDTOValidator : AbstractValidator<CreateCollectionDTO>
    {
        public CreateCollectionDTOValidator()
        {
            Include(new ICollectionDTOValidator());
        }
    }
}
