using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using TestInitProject.Application;
using TestInitProject.Domain.Entities;
using TestInitProject.Infrastructure.Data;

namespace TestInitProject.Infrastructure;

internal class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> CheckUniqueEmailAsync(string email)
    {
        return !await DbContext.Set<User>().AnyAsync(x => x.Email.ToLower() == email.ToLower());
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await DbContext.Set<User>().FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
    }
}
