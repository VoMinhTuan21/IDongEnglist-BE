using AutoMapper;
using IDonEnglist.Application.DTOs.Media.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Medias.Commands
{
    public class UpdateMedia : IRequest<int>
    {
        public UpdateMediaDTO UpdateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class UpdateMediaHandler : IRequestHandler<UpdateMedia, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateMediaHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<int> Handle(UpdateMedia request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await ValidateRequest(request);

                var media = await _unitOfWork.MediaRepository.GetByIdAsync(request.UpdateData.Id);

                if (media?.PublicId != request.UpdateData.PublicId && request.UpdateData.PublicId is not null)
                {
                    await _mediator.Send(new DeleteFile { PublicId = media?.PublicId });
                }

                var updatedMedia = _mapper.Map(request.UpdateData, media);

                await _unitOfWork.MediaRepository.UpdateAsync(updatedMedia, request.CurrentUser);
                await _unitOfWork.Save();

                return request.UpdateData.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private async Task ValidateRequest(UpdateMedia request)
        {
            var validator = new UpdateMediaDTOValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var exist = await _unitOfWork.MediaRepository.ExistsAsync(request.UpdateData.Id);

            if (!exist)
            {
                throw new NotFoundException(nameof(Media), request.UpdateData.Id);
            }
        }
    }
}
