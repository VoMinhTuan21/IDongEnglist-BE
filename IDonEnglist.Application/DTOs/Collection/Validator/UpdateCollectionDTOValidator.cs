using FluentValidation;

namespace IDonEnglist.Application.DTOs.Collection.Validator
{
    public class UpdateCollectionDTOValidator : AbstractValidator<UpdateCollectionDTO>
    {
        public UpdateCollectionDTOValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must greater than 0.");

            Include(new ICollectionDTOValidator());
        }
    }
}
