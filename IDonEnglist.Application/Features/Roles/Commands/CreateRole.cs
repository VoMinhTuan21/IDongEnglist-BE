using AutoMapper;
using IDonEnglist.Application.DTOs.Role;
using IDonEnglist.Application.DTOs.Role.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Application.Utils;
using IDonEnglist.Application.ViewModels.Role;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Roles.Commands
{
    public class CreateRole : IRequest<RoleViewModel>
    {
        public CreateRoleDTO createData { get; set; }
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
            var validator = new CreateRoleDTOValidator();
            var validationResult = await validator.ValidateAsync(request.createData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            if (string.IsNullOrEmpty(request.createData.Code))
            {
                request.createData.Code = SlugGenerator.GenerateSlug(request.createData.Name);
            }

            var role = await _unitOfWork.RoleRepository.AddAsync(_mapper.Map<Role>(request.createData));
            await _unitOfWork.Save();

            return _mapper.Map<RoleViewModel>(role);
        }
    }
}
