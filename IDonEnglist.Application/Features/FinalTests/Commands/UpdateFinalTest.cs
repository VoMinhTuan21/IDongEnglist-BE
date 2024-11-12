using AutoMapper;
using IDonEnglist.Application.DTOs.FinalTest;
using IDonEnglist.Application.DTOs.FinalTest.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.FinalTests.Commands
{
    public class UpdateFinalTest : IRequest<int>
    {
        public UpdateFinalTestDTO UpdateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class UpdateFinalTestHandler : IRequestHandler<UpdateFinalTest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFinalTestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateFinalTest request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await ValidateRequest(request);

                var oldFinalTest = await _unitOfWork.FinalTestRepository.GetByIdAsync(request.UpdateData.Id)
                    ?? throw new NotFoundException(nameof(FinalTest), request.UpdateData.Id);

                var temp = _mapper.Map(request.UpdateData, oldFinalTest);
                temp.Code = SlugGenerator.GenerateSlug(request.UpdateData.Name);

                await _unitOfWork.FinalTestRepository.UpdateAsync(temp, request.CurrentUser);

                await _unitOfWork.Save();

                await _unitOfWork.CommitTransactionAsync();

                return temp.Id;

            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        private async Task ValidateRequest(UpdateFinalTest request)
        {
            var validator = new UpdateFinalTestDTOValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var exist = await _unitOfWork.FinalTestRepository
                .GetOneAsync(ft => ft.Code == SlugGenerator.GenerateSlug(request.UpdateData.Name)
                                    && ft.Id != request.UpdateData.Id);

            if (exist is not null)
            {
                throw new BadRequestException("Please use another name");
            }
        }
    }
}
