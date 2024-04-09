using MediatR;

namespace TestInitProject.Application;

public class LoginCommand(string Email) : IRequest<string>
{
    public string Email { get; set; } = Email;
};

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IJwtProvider _jwtProvider;
    public LoginCommandHandler(
        ICustomerRepository customerRepository,
        IJwtProvider jwtProvider)
    {
        _customerRepository = customerRepository;
        _jwtProvider = jwtProvider;
    }
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByEmailAsync(request.Email);

        if (customer is null)
        {
            return string.Empty;
        }

        string token = _jwtProvider.Generate(customer);

        return token;
    }
}
