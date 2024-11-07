using FluentValidation;
using IDonEnglist.Application.DTOs.Pagination.Validators;

namespace IDonEnglist.Application.DTOs.Collection.Validator
{
    public class GetPaginationCollectionsDTOValidator : AbstractValidator<GetPaginationCollectionsDTO>
    {
        public GetPaginationCollectionsDTOValidator()
        {
            Include(new PaginationDTOValidator(typeof(IDonEnglist.Domain.Collection)));

            RuleFor(p => p.CategoryId)
                .GreaterThan(0).When(p => p.CategoryId != null).WithMessage("{PropertyName} must greater than {ComparisonValue}");
        }
    }
}
