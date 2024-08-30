using AutoMapper;
using IDonEnglist.Application.DTOs.Role;
using IDonEnglist.Application.DTOs.Role.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.Role;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.Roles.Commands
{
    public class CreateRole : IRequest<RoleViewModel>
    {
        public CreateRoleDTO CreateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class CreateRoleHandler : IRequestHandler<CreateRole, RoleViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<RoleViewModel> Handle(CreateRole request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            SetDefaultCodeIfEmpty(request);

            await CheckForDuplicateNameOrCode(request);

            var temp = _mapper.Map<Role>(request.CreateData);
            temp.CreatedBy = request.CurrentUser.Id;

            var role = await _unitOfWork.RoleRepository.AddAsync(temp);

            await _unitOfWork.Save();

            await SaveRolePermission(request, role);

            await _unitOfWork.Save();

            var newRole = await _unitOfWork.RoleRepository.GetByIdAsync(
                role.Id,
                query => query.Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                    .ThenInclude(p => p.Parent)
            );

            return _mapper.Map<RoleViewModel>(newRole);
        }

        private async Task ValidateRequest(CreateRole request)
        {
            var validator = new CreateRoleDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            await CheckPermissionIdExist(request);
        }

        private void SetDefaultCodeIfEmpty(CreateRole request)
        {
            if (string.IsNullOrEmpty(request.CreateData.Code))
            {
                request.CreateData.Code = SlugGenerator.GenerateSlug(request.CreateData.Name);
            }
        }

        private async Task CheckForDuplicateNameOrCode(CreateRole request)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.GetOneAsync(
                c => c.Name == request.CreateData.Name || c.Code == request.CreateData.Code);

            if (existingCategory != null)
            {
                throw new BadRequestException("Name or Code has been used.");
            }
        }

        private async Task CheckPermissionIdExist(CreateRole request)
        {
            if (request.CreateData.PermissionIds == null || request.CreateData.PermissionIds.Count == 0) { return; }

            foreach (var id in request.CreateData.PermissionIds)
            {
                var exist = await _unitOfWork.PermissionRepository.ExistsAsync(id);

                if (!exist)
                {
                    throw new NotFoundException(nameof(Permission), id);
                }
            }
        }

        private async Task SaveRolePermission(CreateRole request, Role role)
        {
            if (request.CreateData.PermissionIds == null || request.CreateData.PermissionIds.Count == 0) { return; }

            var rolePermissions = request.CreateData.PermissionIds.Select(id => new RolePermission
            {
                RoleId = role.Id,
                PermissionId = id,
                CreatedBy = request.CurrentUser.Id
            }).ToList();

            await _unitOfWork.RolePermissionRepository.AddRangeAsync(rolePermissions);
        }
    }
}
