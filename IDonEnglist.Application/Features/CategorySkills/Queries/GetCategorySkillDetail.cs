using AutoMapper;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.CategorySkill;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.CategorySkills.Queries
{
    public class GetCategorySkillDetail : IRequest<CategorySkillWithParentViewModel>
    {
        public int Id { get; set; }
    }

    public class GetCategorySkillDetailHandler : IRequestHandler<GetCategorySkillDetail, CategorySkillWithParentViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategorySkillDetailHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategorySkillWithParentViewModel> Handle(GetCategorySkillDetail request, CancellationToken cancellationToken)
        {
            ValidateRequest(request);

            var categorySkill = await _unitOfWork.CategorySkillRepository.GetByIdAsync(request.Id, include: query => query.Include(ck => ck.Category))
                ?? throw new NotFoundException(nameof(CategorySkill), request.Id);

            var result = _mapper.Map<CategorySkillWithParentViewModel>(categorySkill);

            return result;
        }

        private void ValidateRequest(GetCategorySkillDetail request)
        {
            if (request.Id <= 0)
            {
                throw new BadRequestException("Id must be greater than 0");
            }
        }
    }
}
