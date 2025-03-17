using IDonEnglist.Application.DTOs.Test;
using IDonEnglist.Application.DTOs.Test.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Tests.Commands
{
    public class UpdateTest : IRequest<int>
    {
        public UpdateTestFromCommandDTO? UpdateDataFromCommand { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class UpdateTestHandler : IRequestHandler<UpdateTest, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(UpdateTest request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (request.UpdateDataFromCommand != null)
                {
                    var test = await _unitOfWork.TestRepository.GetByIdAsync(request.UpdateDataFromCommand.Id)
                        ?? throw new NotFoundException(nameof(Test), request.UpdateDataFromCommand.Id);

                    test.AudioId = request.UpdateDataFromCommand.AudioId;

                    await _unitOfWork.TestRepository.UpdateAsync(test, request.CurrentUser);
                    await _unitOfWork.Save();

                    return test.Id;
                }

                return 0;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        private async Task ValidateRequest(UpdateTest request)
        {
            if (request.UpdateDataFromCommand is not null)
            {
                var validator = new UpdateTestFromCommandDTOValidator();
                var validationResult = await validator.ValidateAsync(request.UpdateDataFromCommand);

                if (!validationResult.IsValid)
                {
                    throw new ValidatorException(validationResult);
                }

                var existAudio = await _unitOfWork.MediaRepository.ExistsAsync(request.UpdateDataFromCommand.AudioId);
                if (!existAudio)
                {
                    throw new NotFoundException(nameof(Test), request.UpdateDataFromCommand.AudioId);
                }
            }
        }
    }
}
