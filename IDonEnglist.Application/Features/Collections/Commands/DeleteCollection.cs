using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Common.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Collections.Commands
{
    public class DeleteCollection : IRequest<int>
    {
        public BaseDTO DeleteData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class DeleteCollectionHandler : IRequestHandler<DeleteCollection, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCollectionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(DeleteCollection request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.CollectionRepository.DeleteAsync(request.DeleteData.Id, request.CurrentUser);
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
        private async Task ValidateRequest(DeleteCollection request)
        {
            var validator = new BaseDTOValidator();
            var validationResult = await validator.ValidateAsync(request.DeleteData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var exist = await _unitOfWork.CollectionRepository.ExistsAsync(request.DeleteData.Id);
            if (!exist)
            {
                throw new NotFoundException(nameof(Collection), request.DeleteData.Id);
            }
        }
    }
}
