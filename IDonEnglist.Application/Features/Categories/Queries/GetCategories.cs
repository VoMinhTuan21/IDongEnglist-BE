using AutoMapper;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.Categories.Queries
{
    public class GetCategories : IRequest<IReadOnlyList<CategoryDetailViewModel>>
    {
        public bool IsHierarchy { get; set; } = false;
    }

    public class GetCategoriesHandler : IRequestHandler<GetCategories, IReadOnlyList<CategoryDetailViewModel>>
    {
        private readonly ICategoryRepository _categoryRespository;
        private readonly IMapper _mapper;

        public GetCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRespository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<CategoryDetailViewModel>> Handle(GetCategories request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRespository.GetAllListAsync(c => c.ParentId == null, null, true,
                false, query => query.Include(c => c.Children)
                                        .ThenInclude(c => c.Skills.Where(c => c.DeletedDate == null && c.DeletedBy == null))
            );

            return _mapper.Map<IReadOnlyList<CategoryDetailViewModel>>(categories);
        }
    }
}
