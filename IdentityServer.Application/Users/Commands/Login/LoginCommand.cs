using MediatR;
using Microsoft.AspNetCore.Mvc;
using IdentityServer.Application.Common.Exceptions;

namespace IdentityServer.Application;

public class LoginCommand(string email, string password) : IRequest<string>
{

    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
};

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _UserRepository;
    private readonly IJwtProvider _jwtProvider;
    public LoginCommandHandler(
        IUserRepository UserRepository,
        IJwtProvider jwtProvider)
    {
        _UserRepository = UserRepository;
        _jwtProvider = jwtProvider;
    }
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _UserRepository.GetUserByEmailAsync(request.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword))
        {
            throw new UnauthorizationException();
        }

        string token = await _jwtProvider.GenerateAsync(user);

        return token;
    }
}
