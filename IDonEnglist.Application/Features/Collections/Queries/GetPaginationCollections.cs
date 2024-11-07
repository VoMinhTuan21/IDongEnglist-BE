using AutoMapper;
using IDonEnglist.Application.DTOs.Collection;
using IDonEnglist.Application.DTOs.Collection.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Pagination;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.Collection;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IDonEnglist.Application.Features.Collections.Queries
{
    public class GetPaginationCollections : IRequest<PaginatedList<CollectionViewModel>>
    {
        public GetPaginationCollectionsDTO Filter { get; set; }
    }

    public class GetPaginationCollectionsHandler : IRequestHandler<GetPaginationCollections, PaginatedList<CollectionViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPaginationCollectionsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedList<CollectionViewModel>> Handle(GetPaginationCollections request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            Expression<Func<Collection, bool>> filter = cl => true;

            if (request.Filter.CategoryId != null)
            {
                filter = filter.And(cl => cl.CategoryId == request.Filter.CategoryId);
            }

            if (!string.IsNullOrEmpty(request.Filter.Keywords))
            {
                var keywords = request.Filter.Keywords.ToLower().Split(' ');
                filter = filter.And(cl => keywords.All(k => cl.Name.ToLower().Contains(k)));
            }

            var paginatedCollections = await _unitOfWork.CollectionRepository.GetPaginatedListAsync(
                    filter: filter,
                    pageNumber: request.Filter.PageNumber,
                    pageSize: request.Filter.PageSize,
                    include: (query) => query.Include(cl => cl.Thumbnail)
                );

            PaginatedList<CollectionViewModel> result = new PaginatedList<CollectionViewModel>
            {
                Items = _mapper.Map<List<CollectionViewModel>>(paginatedCollections.Items),
                PageIndex = paginatedCollections.PageIndex,
                PageSize = paginatedCollections.PageSize,
                TotalPages = paginatedCollections.TotalPages,
                TotalRecords = paginatedCollections.TotalRecords,
            };

            return result;
        }
        private async Task ValidateRequest(GetPaginationCollections request)
        {
            var validator = new GetPaginationCollectionsDTOValidator();
            var validationResult = await validator.ValidateAsync(request.Filter);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            if (request.Filter.CategoryId != null)
            {
                var existed = await _unitOfWork.CategoryRepository.ExistsAsync(request.Filter.CategoryId ?? 0);

                if (!existed)
                {
                    throw new NotFoundException(nameof(Category), request.Filter.CategoryId ?? 0);
                }
            }
        }
    }
}
