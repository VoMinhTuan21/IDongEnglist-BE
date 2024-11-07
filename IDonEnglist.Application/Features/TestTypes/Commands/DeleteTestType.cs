using IDonEnglist.Application.DTOs.Common.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.TestTypes.Commands
{
    public class DeleteTestType : IRequest<int>
    {
        public int TestTypeId { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class DeleteTestTypeHandler : IRequestHandler<DeleteTestType, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTestTypeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteTestType request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await ValidateRequest(request);

                await _unitOfWork.TestTypeRepository.DeleteAsync(request.TestTypeId, request.CurrentUser);

                await _unitOfWork.Save();
                await _unitOfWork.CommitTransactionAsync();

                return request.TestTypeId;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private async Task ValidateRequest(DeleteTestType request)
        {
            var validator = new BaseDTOValidator();
            var validationResult = await validator.ValidateAsync(new DTOs.Common.BaseDTO { Id = request.TestTypeId });

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var isExisted = await _unitOfWork.TestTypeRepository.ExistsAsync(request.TestTypeId);

            if (!isExisted)
            {
                throw new NotFoundException(nameof(TestType), request.TestTypeId);
            }
        }
    }
}
