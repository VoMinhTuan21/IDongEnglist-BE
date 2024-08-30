using FluentValidation;

namespace IDonEnglist.Application.DTOs.Pagination.Validators
{
    public class PaginationDTOValidator : AbstractValidator<PaginationDTO>
    {
        private readonly Type _targetType;

        public PaginationDTOValidator(Type targetType)
        {
            _targetType = targetType;

            RuleFor(x => x.SortBy)
                .Must(BeAValidPropertyName)
                .WithMessage($"SortBy must be a valid property name of {_targetType.Name}.")
                .When(x => !string.IsNullOrEmpty(x.SortBy));
        }

        private bool BeAValidPropertyName(string sortBy)
        {
            return _targetType.GetProperties().Any(p => p.Name.Equals(sortBy, StringComparison.OrdinalIgnoreCase));
        }
    }
}
