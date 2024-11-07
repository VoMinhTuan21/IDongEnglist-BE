using AutoMapper;
using IDonEnglist.Application.DTOs.Media;
using IDonEnglist.Application.DTOs.Media.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.Media;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Medias.Commands
{
    public class CreateMedia : IRequest<MediaViewModel>
    {
        public CreateMediaDTO CreateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class CreateMediaHandler : IRequestHandler<CreateMedia, MediaViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMediaHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<MediaViewModel> Handle(CreateMedia request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await ValidateRequest(request);

                var newMedia = _mapper.Map<Media>(request.CreateData);

                await _unitOfWork.MediaRepository.AddAsync(newMedia, request.CurrentUser);
                await _unitOfWork.Save();

                return _mapper.Map<MediaViewModel>(newMedia);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private async Task ValidateRequest(CreateMedia request)
        {
            var validator = new CreateMediaDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }
        }
    }
}
