using AutoMapper;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.ViewModels.Role;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.Roles.Queries
{
    public class GetRole : IRequest<RoleViewModel>
    {
        public int Id { get; set; }
        public bool WithDeleted { get; set; } = false;
    }

    public class GetRoleHandler : IRequestHandler<GetRole, RoleViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RoleViewModel> Handle(GetRole request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(request.Id,
                query => query.Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                    .ThenInclude(p => p.Parent));

            if (role == null)
            {
                throw new NotFoundException(nameof(role), request.Id);
            }

            if (!request.WithDeleted && role.DeletedDate != null && role.DeletedBy != null)
            {
                throw new BadRequestException("This role has been deleted.");
            }

            return _mapper.Map<RoleViewModel>(role);
        }
    }
}
