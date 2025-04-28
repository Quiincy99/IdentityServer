
using IdentityServer.Domain.Entities;

namespace IdentityServer.Application;

public interface IUserRepository : IRepository<User>
{
    public Task<bool> CheckUniqueEmailAsync(string email);

    public Task<User?> GetUserByEmailAsync(string email);
}
