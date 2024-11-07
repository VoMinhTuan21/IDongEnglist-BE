using CloudinaryDotNet.Actions;
using IDonEnglist.Application.Utils;
using IDonEnglist.CloudinaryService;
using MediatR;

namespace IDonEnglist.Application.Features.Medias.Commands
{
    public class DeleteFile : IRequest<string>
    {
        public string PublicId { get; set; }
    }

    public class DeleteFileHandler : IRequestHandler<DeleteFile, string>
    {
        private readonly ICloudinaryService _cloudinaryService;

        public DeleteFileHandler(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }
        public async Task<string> Handle(DeleteFile request, CancellationToken cancellationToken)
        {
            var isAudioFile = AudioFileValidator.IsAudioFile(request.PublicId);
            var deleteResult = await _cloudinaryService.DeleteFileAsync(request.PublicId, isAudioFile ? ResourceType.Raw : ResourceType.Image);

            return request.PublicId;
        }
    }
}
