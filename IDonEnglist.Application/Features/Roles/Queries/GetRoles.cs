using AutoMapper;
using IDonEnglist.Application.DTOs.Pagination.Validators;
using IDonEnglist.Application.DTOs.Role;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Pagination;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.Role;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IDonEnglist.Application.Features.Roles.Queries
{
    public class GetRoles : IRequest<PaginatedList<RoleViewModel>>
    {
        public GetRolesDTO FilterData { get; set; }
    }

    public class GetRolesHandler : IRequestHandler<GetRoles, PaginatedList<RoleViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRolesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedList<RoleViewModel>> Handle(GetRoles request, CancellationToken cancellationToken)
        {
            var validator = new PaginationDTOValidator(typeof(Role));
            var validationResult = await validator.ValidateAsync(request.FilterData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            Expression<Func<Role, bool>> filter = r => true;
            Expression<Func<Role, object>> sorting = null;

            if (!string.IsNullOrEmpty(request.FilterData.KeyWord))
            {
                filter = filter.And(r => r.Name.Contains(request.FilterData.KeyWord) ||
                                     r.Code.Contains(request.FilterData.KeyWord) ||
                                     (r.Description != null && r.Description.Contains(request.FilterData.KeyWord)));
            }

            if (!string.IsNullOrEmpty(request.FilterData.SortBy))
            {
                sorting = r => EF.Property<object>(r, request.FilterData.SortBy);
            }

            var pagedList = await _unitOfWork.RoleRepository.GetPaginatedListAsync(
                filter,
                sorting,
                request.FilterData.Ascending,
                request.FilterData.PageNumber,
                request.FilterData.PageSize,
                request.FilterData.WithDeleted
            );

            var result = new PaginatedList<RoleViewModel>
            {
                PageSize = pagedList.PageSize,
                PageNumber = pagedList.PageNumber,
                TotalRecords = pagedList.TotalRecords,
                TotalPages = pagedList.TotalPages,
                Items = _mapper.Map<List<RoleViewModel>>(pagedList.Items)
            };

            return result;
        }
    }
}
