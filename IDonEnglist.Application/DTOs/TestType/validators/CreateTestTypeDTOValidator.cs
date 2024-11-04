using FluentValidation;
using IDonEnglist.Application.DTOs.TestPart.Validators;
using IDonEnglist.Application.Utils;

namespace IDonEnglist.Application.DTOs.TestType.validators
{
    public class CreateTestTypeDTOValidator : AbstractValidator<CreateTestTypeDTO>
    {
        public CreateTestTypeDTOValidator()
        {
            Include(new ITestTypeDTOValidator());

            RuleFor(p => p.Duration)
            .Must((testType, duration) => testType.Parts.Sum(item => item.Duration) == duration)
            .WithMessage("The total duration of parts must equal {PropertyValue}.");

            RuleFor(p => p.Questions)
            .Must((testType, questions) => testType.Parts.Sum(item => item.Questions) == questions)
            .WithMessage("The total questions of parts must equal {PropertyValue}.");

            RuleForEach(p => p.Parts).SetValidator(new CreateTestPartDTOValidator());
            RuleFor(p => p.Parts).Must(parts =>
            {
                var nameSet = new HashSet<string>();
                var codeSet = new HashSet<string>();
                parts.ForEach(x => { nameSet.Add(x.Name); codeSet.Add(SlugGenerator.GenerateSlug(x.Name)); });

                return nameSet.Count == parts.Count && codeSet.Count == parts.Count;
            }).WithMessage("The name of each part must be unique")
            .Must(parts =>
            {
                var totalParts = parts.Count;
                return parts.All(p => p.Order <= totalParts);
            }).WithMessage("The order value of each part should not greater than the number of parts")
            .Must(parts =>
            {
                var orderSet = new HashSet<int>();
                parts.ForEach(x => orderSet.Add(x.Order));

                return orderSet.Count == parts.Count;
            }).WithMessage("The order value of each part must be unique");
        }
    }
}
