using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Common.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.FinalTests.Commands
{
    public class DeleteFinalTest : IRequest<int>
    {
        public BaseDTO DeleteData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class DeleteFinalTestHandler : IRequestHandler<DeleteFinalTest, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFinalTestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteFinalTest request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await ValidateRequest(request);
                await _unitOfWork.FinalTestRepository.DeleteAsync(request.DeleteData.Id, request.CurrentUser);
                await _unitOfWork.Save();

                await _unitOfWork.CommitTransactionAsync();

                return request.DeleteData.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        private async Task ValidateRequest(DeleteFinalTest request)
        {
            var validator = new BaseDTOValidator();
            var validationResult = await validator.ValidateAsync(request.DeleteData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var exist = await _unitOfWork.FinalTestRepository.ExistsAsync(request.DeleteData.Id);

            if (!exist)
            {
                throw new NotFoundException(nameof(FinalTest), request.DeleteData.Id);
            }
        }
    }
}
