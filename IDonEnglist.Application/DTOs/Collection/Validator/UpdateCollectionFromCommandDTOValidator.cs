using FluentValidation;

namespace IDonEnglist.Application.DTOs.Collection.Validator
{
    public class UpdateCollectionFromCommandDTOValidator : AbstractValidator<UpdateCollectionFromCommandDTO>
    {
        public UpdateCollectionFromCommandDTOValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");

            RuleFor(p => p.ThumbnailId)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required")
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");
        }
    }
}
