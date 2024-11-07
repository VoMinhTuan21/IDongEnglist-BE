using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace IDonEnglist.Application.DTOs.Media.Validators
{
    public class UploadAudioDTOValidator : AbstractValidator<UploadAudioDTO>
    {
        private readonly List<string> _allowedAudioTypes = new List<string> { "audio/mpeg", "audio/wav", "audio/mp3" };
        public UploadAudioDTOValidator()
        {
            RuleFor(x => x.Audio)
            .NotNull().WithMessage("Audio file is required.")
            .Must(BeAValidAudioType).WithMessage("Invalid audio file type. Allowed types are: MP3, WAV.")
            .Must(BeAValidFileSize).WithMessage("Audio file size must not exceed 10 MB.");
        }
        private bool BeAValidAudioType(IFormFile file)
        {
            return _allowedAudioTypes.Contains(file.ContentType);
        }

        private bool BeAValidFileSize(IFormFile file)
        {
            return file.Length <= 10 * 1024 * 1024; // 10 MB limit
        }
    }
}
