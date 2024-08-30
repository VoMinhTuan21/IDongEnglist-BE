using AutoMapper;
using IDonEnglist.Application.Models.Pagination;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.Permission;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.Permissions.Queries
{
    public class GetPermissions : IRequest<PaginatedList<PermissionViewModel>>
    {
    }

    public class GetPermissionsHandler : IRequestHandler<GetPermissions, PaginatedList<PermissionViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPermissionsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedList<PermissionViewModel>> Handle(GetPermissions request, CancellationToken cancellationToken)
        {
            var permissions = await _unitOfWork.PermissionRepository.GetPaginatedListAsync(p => p.ParentId == null, null, true, 1, 100, false,
                query => query.Include(p => p.Children));

            var result = new PaginatedList<PermissionViewModel>
            {
                Items = _mapper.Map<List<PermissionViewModel>>(permissions.Items),
                PageIndex = permissions.PageIndex,
                PageSize = permissions.PageSize,
                TotalPages = permissions.TotalPages,
                TotalRecords = permissions.TotalRecords,
            };

            return result;
        }
    }
}
