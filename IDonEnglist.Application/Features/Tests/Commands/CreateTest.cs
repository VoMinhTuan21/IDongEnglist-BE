using AutoMapper;
using IDonEnglist.Application.DTOs.Test;
using IDonEnglist.Application.DTOs.Test.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Features.Tests.Events;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.Test;
using IDonEnglist.Domain;
using IDonEnglist.Domain.Common;
using MediatR;

namespace IDonEnglist.Application.Features.Tests.Commands
{
    public class CreateTest : IRequest<TestMinViewModel>
    {
        public CreateTestDTO CreateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class CreateTestHandler : IRequestHandler<CreateTest, TestMinViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateTestHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<TestMinViewModel> Handle(CreateTest request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var testType = await _unitOfWork.TestTypeRepository.GetOneAsync(tt => tt.CategorySkillId == request.CreateData.CategorySkillId)
                    ?? throw new NotFoundException(nameof(TestType), new { CategorySkillId = request.CreateData.CategorySkillId });

                var categorySkill = await _unitOfWork.CategorySkillRepository.GetByIdAsync(request.CreateData.CategorySkillId);

                var test = new Test
                {
                    Name = request.CreateData.Name,
                    Code = SlugGenerator.GenerateSlug(request.CreateData.Name),
                    FinalTestId = request.CreateData.FinalTestId,
                    TestTypeId = testType.Id,
                    TestTaken = 0
                };

                await _unitOfWork.TestRepository.AddAsync(test, request.CurrentUser);
                await _unitOfWork.Save();

                if (request.CreateData.Audio is not null && categorySkill?.Skill == Skill.Listening)
                {
                    await _mediator.Publish(new TestCreatedNotification
                    {
                        Audio = request.CreateData.Audio,
                        CurrentUser = request.CurrentUser,
                        TestId = test.Id
                    }, cancellationToken);
                }

                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<TestMinViewModel>(test);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        private async Task ValidateRequest(CreateTest request)
        {
            var validator = new CreateTestDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var existCategorySkill = await _unitOfWork.CategorySkillRepository.ExistsAsync(request.CreateData.CategorySkillId);

            if (!existCategorySkill)
            {
                throw new NotFoundException(nameof(CategorySkill), request.CreateData.CategorySkillId);
            }

            var existFinalTest = await _unitOfWork.FinalTestRepository.ExistsAsync(request.CreateData.FinalTestId);

            if (!existFinalTest)
            {
                throw new NotFoundException(nameof(FinalTest), request.CreateData.FinalTestId);
            }

            var categorySkill = await _unitOfWork.CategorySkillRepository.GetByIdAsync(request.CreateData.CategorySkillId);

            if (categorySkill?.Skill == Skill.Listening && request.CreateData.Audio is null)
            {
                throw new BadRequestException("Audio is required.");
            }

            var testType = await _unitOfWork.TestTypeRepository.GetOneAsync(tt => tt.CategorySkillId == request.CreateData.CategorySkillId)
                    ?? throw new NotFoundException(nameof(TestType), new { CategorySkillId = request.CreateData.CategorySkillId });

            var existTest = await _unitOfWork.TestRepository.GetOneAsync(t => t.FinalTestId == request.CreateData.FinalTestId && t.TestTypeId == testType.Id);

            if (existTest is not null)
            {
                throw new BadRequestException("There already have test for this skill");
            }
        }
    }
}
