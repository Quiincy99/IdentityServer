using System;
using IdentityServer.Domain.Entities;

namespace IdentityServer.Application.Common.Interfaces.Repositories;

public interface IPasswordResetTokenRepository : IRepository<PasswordResetToken>
{
}
