using AutoMapper;
using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Categories.Commands
{
    public class DeleteCategory : IRequest<CategoryDTO>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategory, CategoryDTO>
    {
        private readonly ICategoryRepository _categoryRespository;
        private readonly IMapper _mapper;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRespository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> Handle(DeleteCategory request, CancellationToken cancellationToken)
        {
            var category = await _categoryRespository.Get(request.Id) ?? throw new NotFoundException(nameof(Category), request.Id);
            await _categoryRespository.Delete(category);

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
