using MediatR;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Application.Exceptions;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.AuthFeature.Command.ChangePassword
{
    public class ChangePasswordCommandHandler(IUserRepository userRepository) : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            User foundUser = await _userRepository.GetByEmailAsync(request.Email) 
                ?? throw new BadRequestException("User not found!");

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.CurPassword, foundUser.Password);
            if (!isValidPassword) throw new UnAuthorizationException("Wrong password!");

            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword, salt);

            foundUser.Password = hashPassword;
            await _userRepository.UpdateAsync(foundUser);

            return Unit.Value;
        }
    }
}
