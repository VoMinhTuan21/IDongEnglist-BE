using AutoMapper;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.Category;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Categories.Commands
{
    public class DeleteCategory : IRequest<CategoryViewModel>
    {
        public int Id { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategory, CategoryViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryViewModel> Handle(DeleteCategory request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Category), request.Id);
            await _unitOfWork.CategoryRepository.DeleteAsync(request.Id, request.CurrentUser.Id);
            await _unitOfWork.Save();

            return _mapper.Map<CategoryViewModel>(category);
        }
    }
}
