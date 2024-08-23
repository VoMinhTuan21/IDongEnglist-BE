using IDonEnglist.Application.DTOs.User;
using IDonEnglist.Application.DTOs.User.Validators;
using IDonEnglist.Application.Exceptions;
using IDonEnglist.Application.Persistence.Contracts;
using MediatR;

namespace IDonEnglist.Application.Features.Users.Queries
{
    public class CheckUserExist : IRequest<bool>
    {
        public CheckUserExistDTO checkData { get; set; }
    }

    public class CheckUserExistHandler : IRequestHandler<CheckUserExist, bool>
    {
        private readonly IUserRepository _userRepository;

        public CheckUserExistHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(CheckUserExist request, CancellationToken cancellationToken)
        {
            var validator = new CheckUserExistDTOValidator();
            var validationResult = await validator.ValidateAsync(request.checkData);

            if (!validationResult.IsValid)
            {
                throw new ValidatorException(validationResult);
            }

            var result = await _userRepository.ExistsAsync(request.checkData);

            return result;
        }
    }
}
