using System;
using IdentityServer.Application.Common.Interfaces.Email;
using IdentityServer.Application.Common.Interfaces.Repositories;
using IdentityServer.Domain.Entities;
using MediatR;

namespace IdentityServer.Application.Users.Commands.ResetPasswrod.cs;

public class RequestResetPasswordCommand(string email) : IRequest
{
    public string Email { get; set; } = email;
};

public sealed class ResetPasswordCommandHandler : IRequestHandler<RequestResetPasswordCommand>
{
    private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    public ResetPasswordCommandHandler(
        IPasswordResetTokenRepository passwordResetTokenRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ITokenGenerator tokenGenerator,
        IEmailService emailService)
    {
        _passwordResetTokenRepository = passwordResetTokenRepository;
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
        _unitOfWork = unitOfWork;
        _emailService = emailService;
    }

    public async Task Handle(RequestResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email);

        if (user == null)
        {
            return;
        }

        var token = new PasswordResetToken(Guid.NewGuid())
        {
            UserId = user.Id,
            Token = _tokenGenerator.GenerateToken(),
            ExpiryDate = DateTime.UtcNow.AddHours(2)
        };

        _passwordResetTokenRepository.Add(token);

        await _emailService.SendPasswordResetEmailAsync(user.Email, token.Token);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
