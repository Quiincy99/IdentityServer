using System.Runtime.CompilerServices;

namespace IdentityServer.Application.Common.Interfaces.Auth;

public interface IUserContext
{
    Guid? Id { get; }
    bool IsAnonymous { get; }
    string Email { get; }
}
