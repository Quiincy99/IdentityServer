using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Application;
using IdentityServer.Domain.Entities;
using IdentityServer.Infrastructure.Data;

namespace IdentityServer.Infrastructure;

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
