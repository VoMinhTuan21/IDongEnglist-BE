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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> Handle(CreateCategory request, CancellationToken cancellationToken)
        {

            var validator = new CreateCategoryDTOValidator(_unitOfWork.CategoryRepository);
            var validationResult = await validator.ValidateAsync(request.createData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var category = await _unitOfWork.CategoryRepository.AddAsync(_mapper.Map<Category>(request.createData));
            await _unitOfWork.Save();

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
