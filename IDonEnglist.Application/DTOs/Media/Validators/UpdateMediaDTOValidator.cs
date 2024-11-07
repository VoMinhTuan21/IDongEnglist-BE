using FluentValidation;
using IDonEnglist.Application.Utils;

namespace IDonEnglist.Application.DTOs.Media.Validators
{
    public class UpdateMediaDTOValidator : AbstractValidator<UpdateMediaDTO>
    {
        public UpdateMediaDTOValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must greater than {ComparisonValue}");

            RuleFor(p => p.ContextType)
                .IsInEnum().When(dto => dto.ContextType is not null).WithMessage("{PropertyName} is not valid.");

            RuleFor(p => p.Type)
                .IsInEnum().When(dto => dto.Type is not null).WithMessage("{PropertyName} is not valid.");

            RuleFor(p => p.Url)
                .Must(UrlValidator.IsValidUrl).When(dto => dto.Url is not null).WithMessage("{PropertyName} is not a valid url");
        }
    }
}
