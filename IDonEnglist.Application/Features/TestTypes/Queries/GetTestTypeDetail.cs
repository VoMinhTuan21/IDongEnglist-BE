using AutoMapper;
using IDonEnglist.Application.DTOs.Common;
using IDonEnglist.Application.DTOs.Common.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.TestType;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.TestTypes.Queries
{
    public class GetTestTypeDetail : IRequest<TestTypeDetailViewModel>
    {
        public BaseDTO GetData { get; set; }
    }

    public class GetTestTypeDetailHandler : IRequestHandler<GetTestTypeDetail, TestTypeDetailViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTestTypeDetailHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TestTypeDetailViewModel> Handle(GetTestTypeDetail request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            var testType = await _unitOfWork.TestTypeRepository.GetByIdAsync(
                request.GetData.Id,
                (query) => query.Include(p => p.CategorySkill).ThenInclude(ck => ck.Category)
                                .Include(p => p.TestParts)
            );

            var result = _mapper.Map<TestTypeDetailViewModel>(testType);

            return result;
        }

        private async Task ValidateRequest(GetTestTypeDetail request)
        {
            var validator = new BaseDTOValidator();
            var validationResult = await validator.ValidateAsync(request.GetData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }
        }
    }
}
