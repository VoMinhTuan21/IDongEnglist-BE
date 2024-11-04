using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace IDonEnglist.Application.DTOs.Media.Validators
{
    public class UploadImageDTOValidator : AbstractValidator<UploadImageDTO>
    {
        private readonly List<string> _allowedImageTypes = new List<string> { "image/jpeg", "image/png", "image/gif" };
        public UploadImageDTOValidator()
        {
            RuleFor(x => x.Image)
                .NotNull().WithMessage("Image file is requred")
                .Must(BeAValidImageType).WithMessage("Invalid Image file type. Allowed types are: JPEG, PNG, GIF")
                .Must(BeAValidFileSize).WithMessage("Image file size must not exceed 5 MB");
        }

        private bool BeAValidImageType(IFormFile file)
        {
            return _allowedImageTypes.Contains(file.ContentType);
        }

        private bool BeAValidFileSize(IFormFile file)
        {
            return file.Length <= 5 * 1024 * 1024;
        }
    }
}
