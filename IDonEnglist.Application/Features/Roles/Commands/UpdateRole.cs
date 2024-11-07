using AutoMapper;
using IDonEnglist.Application.DTOs.Role;
using IDonEnglist.Application.DTOs.Role.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Models.Identity;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Services.Interfaces;
using IDonEnglist.Application.ViewModels.Role;
using IDonEnglist.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IDonEnglist.Application.Features.Roles.Commands
{
    public class UpdateRole : IRequest<RoleViewModel>
    {
        public UpdateRoleDTO UpdateData { get; set; }
        public CurrentUser CurrentUser { get; set; }
    }

    public class UpDateRoleHandler : IRequestHandler<UpdateRole, RoleViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRolePermissionService _rolePermissionService;

        public UpDateRoleHandler(IUnitOfWork unitOfWorkd, IMapper mapper, IRolePermissionService rolePermissionService)
        {
            _unitOfWork = unitOfWorkd;
            _mapper = mapper;
            _rolePermissionService = rolePermissionService;
        }
        public async Task<RoleViewModel> Handle(UpdateRole request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            var oldRole = await _unitOfWork.RoleRepository.GetByIdAsync(request.UpdateData.Id)
                ?? throw new NotFoundException(nameof(Role), request.UpdateData.Id);

            oldRole = _mapper.Map(request.UpdateData, oldRole);

            if (request.UpdateData.PermissionIds != null || request.UpdateData.PermissionIds?.Count() != 0)
            {
                await _rolePermissionService.UpdateRolePermissionsAsync(request.UpdateData.Id,
                    request.UpdateData.PermissionIds, request.CurrentUser);
            }

            await _unitOfWork.RoleRepository.UpdateAsync(oldRole, request.CurrentUser);

            await _unitOfWork.Save();

            var updatedRole = await _unitOfWork.RoleRepository
                .GetByIdAsync(
                    request.UpdateData.Id,
                    query => query.Include(r => r.RolePermissions.Where(rp => rp.DeletedBy == null && rp.DeletedDate == null))
                        .ThenInclude(rp => rp.Permission)
                        .ThenInclude(p => p.Parent)
                );

            return _mapper.Map<RoleViewModel>(updatedRole);
        }
        private async Task ValidateRequest(UpdateRole request)
        {
            var validator = new UpdateRoleDTOValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            await CheckForDuplicateName(request);
            await CheckForDuplicateCode(request);
        }
        private async Task CheckForDuplicateName(UpdateRole request)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.GetOneAsync(c => c.Name == request.UpdateData.Name);
            if (existingCategory != null && existingCategory.Id != request.UpdateData.Id)
            {
                throw new BadRequestException("The name has been used.");
            }
        }
        private async Task CheckForDuplicateCode(UpdateRole request)
        {
            var existingCategory = await _unitOfWork.CategoryRepository.GetOneAsync(c => c.Code == request.UpdateData.Code);
            if (existingCategory != null && existingCategory.Id != request.UpdateData.Id)
            {
                throw new BadRequestException("The code has been used.");
            }
        }
    }
}
