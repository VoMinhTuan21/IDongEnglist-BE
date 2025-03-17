using AutoMapper;
using IDonEnglist.Application.DTOs.TestType;
using IDonEnglist.Application.DTOs.TestType.validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Features.TestTypes.Events;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.TestType;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.TestTypes.Commands
{
    public class CreateTestType : IRequest<TestTypeDetailViewModel>
    {
        public CreateTestTypeDTO CreateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class CreateTestTypeHandler : IRequestHandler<CreateTestType, TestTypeDetailViewModel>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly IMediator _meditor;

        public CreateTestTypeHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _meditor = mediator;
        }
        public async Task<TestTypeDetailViewModel> Handle(CreateTestType request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await ValidateRequest(request);

                await CheckForDuplicateName(request.CreateData.Name);

                var existedCategorySkill = await _unitOfWork.CategorySkillRepository.ExistsAsync(request.CreateData.CategorySkillId);
                if (!existedCategorySkill)
                {
                    throw new NotFoundException(nameof(CategorySkill), request.CreateData.CategorySkillId);
                }

                var testType = _mapper.Map<TestType>(request.CreateData);
                testType.Code = SlugGenerator.GenerateSlug(request.CreateData.Name);

                await _unitOfWork.TestTypeRepository.AddAsync(testType, request.CurrentUser);

                await _unitOfWork.Save();

                await _meditor.Publish(new TestTypeCreatedNotification
                {
                    TestTypeId = testType.Id,
                    CreateData = request.CreateData.Parts,
                    CurrentUser = request.CurrentUser,
                }, cancellationToken);

                testType = await _unitOfWork.TestTypeRepository
                    .GetByIdAsync(id: testType.Id,
                        include: query => query.Include(t => t.CategorySkill).ThenInclude(ck => ck.Category)
                                              .Include(p => p.TestParts)
                    );

                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<TestTypeDetailViewModel>(testType);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private async Task ValidateRequest(CreateTestType request)
        {
            var validator = new CreateTestTypeDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var existedTestType = await _unitOfWork.TestTypeRepository.GetOneAsync(p => p.CategorySkillId == request.CreateData.CategorySkillId);
            if (existedTestType is not null)
            {
                throw new BadRequestException("One category skill only have one test type");
            }
        }

        private async Task CheckForDuplicateName(string name)
        {
            var existingTestType = await _unitOfWork.TestTypeRepository.GetOneAsync(
                p => p.Name == name || p.Code == SlugGenerator.GenerateSlug(name));

            if (existingTestType is not null)
            {
                throw new BadRequestException("Name or Code has been used");
            }
        }
    }
}
