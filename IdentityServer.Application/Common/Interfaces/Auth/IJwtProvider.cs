
using IdentityServer.Domain.Entities;

namespace IdentityServer.Application;

public interface IJwtProvider
{
    Task<string> GenerateAsync(User user);
}
