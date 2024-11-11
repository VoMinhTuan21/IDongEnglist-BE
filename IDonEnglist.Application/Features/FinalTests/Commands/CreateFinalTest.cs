using AutoMapper;
using IDonEnglist.Application.DTOs.FinalTest;
using IDonEnglist.Application.DTOs.FinalTest.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.FinalTest;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.FinalTests.Commands
{
    public class CreateFinalTest : IRequest<FinalTestViewModel>
    {
        public CreateFinalTestDTO CreateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class CreateFinalTestHandler : IRequestHandler<CreateFinalTest, FinalTestViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFinalTestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FinalTestViewModel> Handle(CreateFinalTest request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await ValidateRequest(request);

                var temp = _mapper.Map<FinalTest>(request.CreateData);

                temp.Code = SlugGenerator.GenerateSlug(request.CreateData.Name);

                await _unitOfWork.FinalTestRepository.AddAsync(temp);
                await _unitOfWork.Save();

                var finalTest = await _unitOfWork.FinalTestRepository.GetByIdAsync(temp.Id, ft => ft.Include(a => a.Collection))
                    ?? throw new NotFoundException(nameof(FinalTest), temp.Id);

                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<FinalTestViewModel>(finalTest);
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

            var existed = await _unitOfWork.FinalTestRepository.GetOneAsync(p => p.Code == SlugGenerator.GenerateSlug(request.CreateData.Name));

            if (existed is not null)
            {
                throw new BadRequestException("Please use another name.");
            }

            _ = await _unitOfWork.CollectionRepository.GetByIdAsync(request.CreateData.CollectionId)
                ?? throw new NotFoundException(nameof(Collection), request.CreateData.CollectionId);
        }
    }
}
