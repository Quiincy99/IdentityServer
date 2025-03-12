
using TestInitProject.Domain.Entities;

namespace TestInitProject.Application;

public interface IUserRepository : IRepository<User>
{
    public Task<bool> CheckUniqueEmailAsync(string email);

    public Task<User?> GetUserByEmailAsync(string email);
}
