using AutoMapper;
using IDonEnglist.Application.DTOs.User;
using IDonEnglist.Application.DTOs.User.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Persistence.Contracts;
using IDonEnglist.Domain;
using MediatR;

namespace IDonEnglist.Application.Features.Users.Commands
{
    public class CreateUser : IRequest<User>
    {
        public SignUpUserDTO signUpData { get; set; }
    }

    public class CreateUserHandler : IRequestHandler<CreateUser, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<User> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var validator = new SignUpUserDTOValidator();
            var validationResult = await validator.ValidateAsync(request.signUpData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var user = await _userRepository.AddAsync(_mapper.Map<User>(request.signUpData));

            return user;
        }
    }
}
