using AutoMapper;
using IDonEnglist.Application.DTOs.FinalTest;
using IDonEnglist.Application.DTOs.FinalTest.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.FinalTests.Commands
{
    public class CreateFinalTest : IRequest<int>
    {
        public CreateFinalTestDTO CreateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class CreateFinalTestHandler : IRequestHandler<CreateFinalTest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFinalTestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateFinalTest request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await ValidateRequest(request);

                var temp = _mapper.Map<FinalTest>(request.CreateData);

                var count = await _unitOfWork.FinalTestRepository.GetAllListAsync(ft => ft.CollectionId == temp.CollectionId);

                temp.Code = SlugGenerator.GenerateSlug($"Test {count.Count() + 1}");

                await _unitOfWork.FinalTestRepository.AddAsync(temp);
                await _unitOfWork.Save();

                var finalTest = await _unitOfWork.FinalTestRepository.GetByIdAsync(temp.Id, ft => ft.Include(a => a.Collection))
                    ?? throw new NotFoundException(nameof(FinalTest), temp.Id);

                await _unitOfWork.CommitTransactionAsync();

                return finalTest.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        private async Task ValidateRequest(CreateFinalTest request)
        {
            var validator = new ICreateFinalTestDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            _ = await _unitOfWork.CollectionRepository.GetByIdAsync(request.CreateData.CollectionId)
                ?? throw new NotFoundException(nameof(Collection), request.CreateData.CollectionId);
        }
    }
}
