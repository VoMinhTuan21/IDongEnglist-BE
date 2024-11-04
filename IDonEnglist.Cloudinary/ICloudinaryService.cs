using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
namespace IDonEnglist.CloudinaryService
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
        Task<DeletionResult> DeleteFileAsync(string publicId, ResourceType resourceType = ResourceType.Image);
        Task<RawUploadResult> UploadAudioAsync(IFormFile file);
    }
}
