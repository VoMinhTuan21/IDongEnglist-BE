using AutoMapper;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.Categories.Queries
{
    public class GetCategories : IRequest<IReadOnlyList<CategoryViewModel>>
    {
        public bool IsHierarchy { get; set; } = false;
    }

    public class GetCategoriesHandler : IRequestHandler<GetCategories, IReadOnlyList<CategoryViewModel>>
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<CategoryViewModel>> Handle(GetCategories request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllListAsync(
                filter: c => c.ParentId == null,
                ascending: true,
                include: query => query.Include(c => c.Children)
                                        .ThenInclude(c => c.Skills.Where(ck => ck.DeletedDate == null && ck.DeletedBy == null))
            );

            var skillIds = categories.SelectMany(c => c.Children ?? [])
                .SelectMany(c => c.Skills ?? [])
                .Select(s => s.Id)
                .ToList();

            var testTypes = await _unitOfWork.TestTypeRepository.GetAllListAsync(
                filter: t => skillIds.Contains(t.CategorySkillId)
            );

            var testTypeIds = testTypes.Select(tt => tt.CategorySkillId).ToHashSet();

            var result = _mapper.Map<List<CategoryViewModel>>(categories);

            foreach (var category in result)
            {
                foreach (var child in category.Children ?? [])
                {
                    foreach (var skill in child.Skills ?? [])
                    {
                        skill.IsConfigured = testTypeIds.Contains(skill.Id);
                    }
                }
            }

            return result;
        }
    }
}
