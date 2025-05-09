using System;
using IdentityServer.Application.Common.Interfaces.Repositories;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Infrastructure.Data.Repositories;

internal class PasswordResetTokenRepository : Repository<PasswordResetToken>, IPasswordResetTokenRepository
{
    public PasswordResetTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
