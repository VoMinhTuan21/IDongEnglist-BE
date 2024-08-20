using AutoMapper;
using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.DTOs.Category.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Categories.Commands
{
    public class CreateCategory : IRequest<CategoryDTO>
    {
        public CreateCategoryDTO createData { get; set; }
    }

    public class CreateCategoryHandler : IRequestHandler<CreateCategory, CategoryDTO>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> Handle(CreateCategory request, CancellationToken cancellationToken)
        {

            var validator = new CreateCategoryDTOValidator(_categoryRepository);
            var validationResult = await validator.ValidateAsync(request.createData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var category = await _categoryRepository.Add(_mapper.Map<Category>(request.createData));

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
