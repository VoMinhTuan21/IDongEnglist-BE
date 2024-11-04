using CloudinaryDotNet.Actions;
using IDonEnglist.Application.DTOs.Media;
using IDonEnglist.Application.DTOs.Media.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.CloudinaryService;
using MediatR;

namespace IDonEnglist.Application.Features.Medias.Commands
{
    public class UploadImage : IRequest<ImageUploadResult>
    {
        public UploadImageDTO UploadData { get; set; }
    }

    public class UploadImageHandler : IRequestHandler<UploadImage, ImageUploadResult>
    {
        private readonly ICloudinaryService _cloudinaryService;

        public UploadImageHandler(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }
        public async Task<ImageUploadResult> Handle(UploadImage request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            return await _cloudinaryService.UploadImageAsync(request.UploadData.Image);
        }
        private async Task ValidateRequest(UploadImage request)
        {
            var validator = new UploadImageDTOValidator();
            var validationResult = await validator.ValidateAsync(request.UploadData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }
        }
    }
}
