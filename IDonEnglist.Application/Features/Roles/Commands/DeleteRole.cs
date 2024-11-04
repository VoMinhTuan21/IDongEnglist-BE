using AutoMapper;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Services.Interfaces;
using IDonEnglist.Application.ViewModels.Role;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Roles.Commands
{
    public class DeleteRole : IRequest<RoleViewModel>
    {
        public int Id { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class DeleteRoleHandler : IRequestHandler<DeleteRole, RoleViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRolePermissionService _rolePermissionService;

        public DeleteRoleHandler(IUnitOfWork unitOfWork, IMapper mapper, IRolePermissionService rolePermissionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rolePermissionService = rolePermissionService;
        }
        public async Task<RoleViewModel> Handle(DeleteRole request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Role), request.Id);

            var deletedRole = await _unitOfWork.RoleRepository.DeleteAsync(request.Id, request.CurrentUser);
            await _rolePermissionService.DeleteRolePermissionAsync(request.Id, request.CurrentUser);
            await _unitOfWork.Save();

            return _mapper.Map<RoleViewModel>(deletedRole);
        }
    }
}
