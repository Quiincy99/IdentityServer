using MediatR;
using IdentityServer.Application.Common.Exceptions;
using IdentityServer.Application.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Application;

public class ResetPasswordCommand(string token, string newPassword) : IRequest<bool>
{
    public string Token { get; set; } = token;
    public string NewPassword { get; set; } = newPassword;
};

public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
{
    private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ResetPasswordCommandHandler(
        IPasswordResetTokenRepository passwordResetTokenRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _passwordResetTokenRepository = passwordResetTokenRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the reset password command by verifying the token and updating the password.
    /// </summary>
    /// <param name="request">The reset password command containing the token and new password.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
    /// <exception cref="InvalidTokenException">Thrown when the token is invalid or expired.</exception>

    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var token = await _passwordResetTokenRepository.AsQueryable()
            .Include(x => x.User)
            .SingleAsync(x => x.Token == request.Token && x.IsValid())
            ?? throw new InvalidTokenException();

        token.User.UpdatePassword(request.NewPassword);
        token.IsUsed = true;

        _passwordResetTokenRepository.Update(token);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
