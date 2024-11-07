using CloudinaryDotNet.Actions;
using IDonEnglist.Application.DTOs.Media;
using IDonEnglist.Application.DTOs.Media.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.CloudinaryService;
using MediatR;

namespace IDonEnglist.Application.Features.Medias.Commands
{
    public class UploadAudio : IRequest<RawUploadResult>
    {
        public UploadAudioDTO UploadData { get; set; }
    }

    public class UploadAudioHandler : IRequestHandler<UploadAudio, RawUploadResult>
    {
        private readonly ICloudinaryService _cloudinaryService;

        public UploadAudioHandler(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }
        public async Task<RawUploadResult> Handle(UploadAudio request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            return await _cloudinaryService.UploadAudioAsync(request.UploadData.Audio);
        }
        private async Task ValidateRequest(UploadAudio request)
        {
            var validator = new UploadAudioDTOValidator();
            var validationResult = await validator.ValidateAsync(request.UploadData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }
        }
    }
}
