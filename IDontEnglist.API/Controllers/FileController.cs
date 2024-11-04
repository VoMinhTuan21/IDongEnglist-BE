using CloudinaryDotNet.Actions;
using IDonEnglist.Application.DTOs.Media;
using IDonEnglist.Application.Features.Medias.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace IDonEnglist.API.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/image")]
        public async Task<ActionResult<ImageUploadResult>> UploadImage(IFormFile file)
        {
            var command = new UploadImage
            {
                UploadData = new UploadImageDTO
                {
                    Image = file
                }
            };

            var uploadResult = await _mediator.Send(command);

            return Ok(uploadResult);
        }

        [HttpPost("/audio")]
        public async Task<ActionResult<ImageUploadResult>> UploadAudio(IFormFile file)
        {
            var command = new UploadAudio
            {
                UploadData = new UploadAudioDTO
                {
                    Audio = file
                }
            };

            var uploadResult = await _mediator.Send(command);

            return Ok(uploadResult);
        }

        [HttpDelete("{publicId}")]
        public async Task<ActionResult> Delete(string publicId)
        {
            var deleteResult = await _mediator.Send(new DeleteFile { PublicId = HttpUtility.UrlDecode(publicId) });
            return Ok(new { PublicId = deleteResult });
        }
    }
}
