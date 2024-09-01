using AutoMapper;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.Categories.Queries
{
    public class GetCategory : IRequest<CategoryDetailViewModel>
    {
        public int Id { get; set; }
    }

    public class GetCategoryHandler : IRequestHandler<GetCategory, CategoryDetailViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryDetailViewModel> Handle(GetCategory request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository
                .GetOneAsync(c => c.Id == request.Id, false,
                    query => query.Include(c => c.Skills.Where(sk => sk.DeletedBy == null && sk.DeletedDate == null))
                );

            return _mapper.Map<CategoryDetailViewModel>(category);
        }
    }
}
