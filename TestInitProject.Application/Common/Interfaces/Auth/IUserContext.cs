using System.Runtime.CompilerServices;

namespace TestInitProject.Application.Common.Interfaces.Auth;

public interface IUserContext
{
    Guid? Id { get; }
    bool IsAnonymous { get; }
    string Email { get; }
}
