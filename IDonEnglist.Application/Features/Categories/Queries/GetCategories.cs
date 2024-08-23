using AutoMapper;
using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.Persistence.Contracts;
using MediatR;

namespace IDonEnglist.Application.Features.Categories.Queries
{
    public class GetCategories : IRequest<IReadOnlyList<CategoryDTO>>
    {
        public bool IsHierarchy { get; set; } = false;
    }

    public class GetCategoriesHandler : IRequestHandler<GetCategories, IReadOnlyList<CategoryDTO>>
    {
        private readonly ICategoryRepository _categoryRespository;
        private readonly IMapper _mapper;

        public GetCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRespository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<CategoryDTO>> Handle(GetCategories request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRespository.GetAllAsync(request.IsHierarchy);

            return _mapper.Map<IReadOnlyList<CategoryDTO>>(categories);
        }
    }
}
