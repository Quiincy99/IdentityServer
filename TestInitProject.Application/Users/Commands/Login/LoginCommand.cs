using MediatR;

namespace TestInitProject.Application;

public class LoginCommand(string Email) : IRequest<string>
{
    public string Email { get; set; } = Email;
};

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
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

        if (user is null)
        {
            return string.Empty;
        }

        string token = await _jwtProvider.GenerateAsync(user);

        return token;
    }
}
