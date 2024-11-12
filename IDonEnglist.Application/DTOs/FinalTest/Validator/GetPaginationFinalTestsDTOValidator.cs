using FluentValidation;
using IDonEnglist.Application.DTOs.Pagination.Validators;

namespace IDonEnglist.Application.DTOs.FinalTest.Validator
{
    public class GetPaginationFinalTestsDTOValidator : AbstractValidator<GetPaginationFinalTestsDTO>
    {
        public GetPaginationFinalTestsDTOValidator()
        {
            Include(new PaginationDTOValidator(typeof(IDonEnglist.Domain.FinalTest)));

            RuleFor(p => p.CollectionId)
                .GreaterThan(0).When(p => p.CollectionId != null).WithMessage("{PropertyName} must greater than {ComparisonValue}");
        }
    }
}
