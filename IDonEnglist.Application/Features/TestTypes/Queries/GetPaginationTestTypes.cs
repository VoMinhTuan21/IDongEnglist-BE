using AutoMapper;
using IDonEnglist.Application.DTOs.Pagination;
using IDonEnglist.Application.DTOs.Pagination.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Pagination;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.TestType;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IDonEnglist.Application.Features.TestTypes.Queries
{
    public class GetPaginationTestTypes : IRequest<PaginatedList<TestTypeItemListViewModel>>
    {
        public PaginationDTO Filter { get; set; }
    }

    public class GetPaginationTestTypesHandler : IRequestHandler<GetPaginationTestTypes, PaginatedList<TestTypeItemListViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPaginationTestTypesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedList<TestTypeItemListViewModel>> Handle(GetPaginationTestTypes request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            Expression<Func<TestType, object>> sorting = null;

            if (!string.IsNullOrEmpty(request.Filter.SortBy))
            {
                sorting = r => EF.Property<object>(r, request.Filter.SortBy);
            }

            var paginatedTestTypes = await _unitOfWork.TestTypeRepository
                .GetPaginatedListAsync(null, sorting, request.Filter.Ascending,
                    request.Filter.PageNumber, request.Filter.PageSize,
                    request.Filter.WithDeleted, query => query.Include(tt => tt.CategorySkill).ThenInclude(ck => ck.Category));

            var result = new PaginatedList<TestTypeItemListViewModel>
            {
                Items = _mapper.Map<List<TestTypeItemListViewModel>>(paginatedTestTypes.Items),
                PageNumber = paginatedTestTypes.PageNumber,
                PageSize = paginatedTestTypes.PageSize,
                TotalPages = paginatedTestTypes.TotalPages,
                TotalRecords = paginatedTestTypes.TotalRecords,
            };

            return result;
        }

        private async Task ValidateRequest(GetPaginationTestTypes request)
        {
            var validator = new PaginationDTOValidator(typeof(TestType));
            var validationResult = await validator.ValidateAsync(request.Filter);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }
        }
    }
}
