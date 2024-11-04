using FluentValidation;
using IDonEnglist.Application.DTOs.Common.Validator;

namespace IDonEnglist.Application.DTOs.Collection.Validator
{
    public class ICollectionDTOValidator : AbstractValidator<ICollectionDTO>
    {
        public ICollectionDTOValidator()
        {
            RuleFor(a => a.Name)
                .NotNull().NotEmpty().WithMessage("{PropertyName} is required.").When(dto => !IsUpdate(dto));
            RuleFor(a => a.CategoryId)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required.").When(dto => !IsUpdate(dto))
                .GreaterThan(0).WithMessage("{PropertyName} must greater than 0.");
            RuleFor(a => a.Thumbnail)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required.").When(dto => !IsUpdate(dto))
                .SetValidator(new FileDTOValidator());
        }
        private bool IsUpdate(ICollectionDTO dto)
        {
            if (dto.GetType().GetProperty("Id") != null)
            {
                return true;
            }
            return false;
        }
    }
}
