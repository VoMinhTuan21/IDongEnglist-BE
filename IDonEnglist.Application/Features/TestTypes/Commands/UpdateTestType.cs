using AutoMapper;
using IDonEnglist.Application.DTOs.TestType;
using IDonEnglist.Application.DTOs.TestType.validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Features.TestTypes.Events;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.TestType;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.TestTypes.Commands
{
    public class UpdateTestType : IRequest<TestTypeDetailViewModel>
    {
        public UpdateTestTypeDTO UpdateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class UpdateTestTypeHandler : IRequestHandler<UpdateTestType, TestTypeDetailViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateTestTypeHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<TestTypeDetailViewModel> Handle(UpdateTestType request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await ValidateRequest(request);

                var oldTestType = await _unitOfWork.TestTypeRepository
                        .GetByIdAsync(
                            id: request.UpdateData.Id,
                            include: query => query.Include(tt => tt.TestParts.Where(tp => tp.DeletedDate == null && tp.DeletedBy == null))
                        )
                    ?? throw new NotFoundException(nameof(TestType), request.UpdateData.Id);

                var updatedTestType = _mapper.Map(request.UpdateData, oldTestType);

                updatedTestType.Code = Utils.SlugGenerator.GenerateSlug(updatedTestType.Name);

                await _unitOfWork.TestTypeRepository.UpdateAsync(updatedTestType, request.CurrentUser);
                await _unitOfWork.Save();

                await _mediator.Publish(new TestTypeUpdatedNotification
                {
                    CurrentUser = request.CurrentUser,
                    TestTypeId = request.UpdateData.Id,
                    UpdatedParts = request.UpdateData.Parts,
                    OldParts = [.. oldTestType.TestParts]
                });

                var testType = await _unitOfWork.TestTypeRepository.GetOneAsync(
                    filter: p => p.Id == request.UpdateData.Id, false,
                    include: query => query.Include(tt => tt.CategorySkill).ThenInclude(ck => ck.Category)
                                            .Include(p => p.TestParts.Where(tp => tp.DeletedBy == null && tp.DeletedDate == null))
                );

                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<TestTypeDetailViewModel>(testType);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        private async Task ValidateRequest(UpdateTestType request)
        {
            var validator = new UpdateTestTypeDTOValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var existedTestType = await _unitOfWork.TestTypeRepository.GetOneAsync(p => p.CategorySkillId == request.UpdateData.CategorySkillId);
            if (existedTestType != null && existedTestType.Id != request.UpdateData.Id)
            {
                throw new BadRequestException("One category skill only have one test type");
            }
        }
    }
}
