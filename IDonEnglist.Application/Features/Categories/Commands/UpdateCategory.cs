using AutoMapper;
using IDonEnglist.Application.DTOs.Category;
using IDonEnglist.Application.DTOs.Category.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Categories.Commands
{
    public class UpdateCategory : IRequest<CategoryDTO>
    {
        public UpdateCategoryDTO updateData { get; set; }
    }

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategory, CategoryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UpdateCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCategoryDTOValidator(_unitOfWork.CategoryRepository);
            var validationResult = await validator.ValidateAsync(request.updateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var categoryOriginal = await _unitOfWork.CategoryRepository.GetByIdAsync(request.updateData.Id);

            if (categoryOriginal == null)
            {
                throw new NotFoundException(nameof(Category), request.updateData.Id);
            }

            _mapper.Map(request.updateData, categoryOriginal);

            var category = await _unitOfWork.CategoryRepository.UpdateAsync(_mapper.Map<Category>(categoryOriginal));
            await _unitOfWork.Save();

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
