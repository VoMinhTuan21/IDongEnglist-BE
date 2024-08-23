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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> Handle(DeleteCategory request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Category), request.Id);
            await _unitOfWork.CategoryRepository.DeleteAsync(request.Id);
            await _unitOfWork.Save();

            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
