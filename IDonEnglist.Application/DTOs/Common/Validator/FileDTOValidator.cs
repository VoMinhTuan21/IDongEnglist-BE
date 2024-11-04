using FluentValidation;
using IDonEnglist.Application.Utils;

namespace IDonEnglist.Application.DTOs.Common.Validator
{
    public class FileDTOValidator : AbstractValidator<FileDTO>
    {
        public FileDTOValidator()
        {
            RuleFor(f => f.PublicId)
                .NotEmpty().NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(f => f.Url)
                .Must(UrlValidator.IsValidUrl).WithMessage("{PropertyName} is not a valid Url.");
        }
    }
}
