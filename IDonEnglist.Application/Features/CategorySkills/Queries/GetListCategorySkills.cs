using AutoMapper;
using IDonEnglist.Application.DTOs.CategorySkill;
using IDonEnglist.Application.DTOs.CategorySkill.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.CategorySkill;
using IDonEnglist.Domain;
using MediatR;
using System.Linq.Expressions;

namespace IDonEnglist.Application.Features.CategorySkills.Queries
{
    public class GetListCategorySkills : IRequest<List<CategorySkillMiniViewModel>>
    {
        public GetListCategorySkillsDTO Fitler { get; set; }
    }

    public class GetListCategorySkillsHandler : IRequestHandler<GetListCategorySkills, List<CategorySkillMiniViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetListCategorySkillsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<CategorySkillMiniViewModel>> Handle(GetListCategorySkills request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            Expression<Func<CategorySkill, bool>> filter = ck => true;

            if (request.Fitler.CollectionId.HasValue)
            {
                var collection = await _unitOfWork.CollectionRepository.GetByIdAsync(request.Fitler.CollectionId ?? 0)
                    ?? throw new NotFoundException(nameof(Collection), request.Fitler.CollectionId);

                filter = filter.And(cl => cl.CategoryId == collection.CategoryId);
            }

            if (request.Fitler.FinalTestId.HasValue)
            {
                var usedTestTypeIds = (
                    await _unitOfWork.TestRepository
                                    .GetAllListAsync(t => t.FinalTestId == request.Fitler.FinalTestId
                                                         && t.DeletedDate == null && t.DeletedBy == null)
                   ).Select(t => t.TestTypeId).ToList();

                filter = filter.And(cl => !cl.TestTypes.Any(tt => usedTestTypeIds.Contains(tt.Id)));
            }

            var categorySkills = await _unitOfWork.CategorySkillRepository.GetAllListAsync(filter: filter);

            return _mapper.Map<List<CategorySkillMiniViewModel>>(categorySkills);
        }
        private async Task ValidateRequest(GetListCategorySkills request)
        {
            var validator = new GetListCategorySkillsDTOValidator();
            var validationResult = await validator.ValidateAsync(request.Fitler);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }
        }
    }
}
