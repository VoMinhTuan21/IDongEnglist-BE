using AutoMapper;
using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Categories.Queries
{
    public class GetCategory : IRequest<CategoryDTO>
    {
        public int Id { get; set; }
    }

    public class GetCategoryHandler : IRequestHandler<GetCategory, CategoryDTO>
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public GetCategoryHandler(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> Handle(GetCategory request, CancellationToken cancellationToken)
        {
            var category = _categoryRepository.Get(request.Id);

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
