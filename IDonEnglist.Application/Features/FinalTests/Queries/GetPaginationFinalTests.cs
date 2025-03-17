using AutoMapper;
using IDonEnglist.Application.DTOs.FinalTest;
using IDonEnglist.Application.DTOs.FinalTest.Validator;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Pagination;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.FinalTest;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IDonEnglist.Application.Features.FinalTests.Queries
{
    public class GetPaginationFinalTests : IRequest<object>
    {
        public GetPaginationFinalTestsDTO FilterData { get; set; }
    }

    public class GetPaginationFinalTestsHandler : IRequestHandler<GetPaginationFinalTests, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPaginationFinalTestsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(GetPaginationFinalTests request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            Expression<Func<FinalTest, bool>> filter = ft => true;

            if (request.FilterData.CollectionId != null)
            {
                filter = filter.And(ft => ft.CollectionId == request.FilterData.CollectionId);
            }

            if (request.FilterData.Keywords != null)
            {
                var keywords = request.FilterData.Keywords.ToLower().Split(' ');
                filter = filter.And(cl => keywords.All(k => cl.Name.ToLower().Contains(k)));
            }

            if (request.FilterData.ForCreateTest)
            {
                var testTypeIds =
                    (await _unitOfWork.CollectionRepository.GetByIdAsync(
                         request.FilterData.CollectionId ?? 0,
                         query => query.Include(c => c.Category)
                                      .ThenInclude(c => c.Skills)
                                      .ThenInclude(ck => ck.TestTypes.Where(
                                                       tt => tt.DeletedBy == null &&
                                                             tt.DeletedDate == null))))
                        ?.Category?.Skills?.Select(ck => ck.TestTypes?[0]?.Id) ??
                    [];

                filter = filter.And(
                    ft => !(ft.Tests.Where(t => t.DeletedBy == null && t.DeletedDate == null)
                                .All(t => testTypeIds.Contains(t.TestTypeId)) &&
                            ft.Tests.Where(t => t.DeletedBy == null && t.DeletedDate == null)
                                    .Count() == testTypeIds.Count()));
            }


            var paginatedFinalTests = await _unitOfWork.FinalTestRepository.GetPaginatedListAsync(
                    filter: filter,
                    pageNumber: request.FilterData.PageNumber,
                    pageSize: request.FilterData.PageSize,
                    include: ft => ft.Include(p => p.Collection)
                );

            if (request.FilterData.ForCreateTest)
            {
                PaginatedList<FinalTestViewModelMin> result = new PaginatedList<FinalTestViewModelMin>
                {
                    Items = _mapper.Map<List<FinalTestViewModelMin>>(paginatedFinalTests.Items),
                    PageNumber = paginatedFinalTests.PageNumber,
                    PageSize = paginatedFinalTests.PageSize,
                    TotalPages = paginatedFinalTests.TotalPages,
                    TotalRecords = paginatedFinalTests.TotalRecords,
                };

                return result;
            }
            else
            {
                PaginatedList<FinalTestViewModel> result = new PaginatedList<FinalTestViewModel>
                {
                    Items = _mapper.Map<List<FinalTestViewModel>>(paginatedFinalTests.Items),
                    PageNumber = paginatedFinalTests.PageNumber,
                    PageSize = paginatedFinalTests.PageSize,
                    TotalPages = paginatedFinalTests.TotalPages,
                    TotalRecords = paginatedFinalTests.TotalRecords,
                };

                return result;
            }
        }
        private async Task ValidateRequest(GetPaginationFinalTests request)
        {
            var validator = new GetPaginationFinalTestsDTOValidator();
            var validationResult = await validator.ValidateAsync(request.FilterData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }
        }
    }
}
