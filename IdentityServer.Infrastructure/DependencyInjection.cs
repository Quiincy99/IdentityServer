using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using IdentityServer.Application;
using IdentityServer.Infrastructure;
using IdentityServer.Infrastructure.Data;
using IdentityServer.Infrastructure.Data.Interceptors;
using IdentityServer.Application.Common.Interfaces.Email;
using IdentityServer.Infrastructure.Email;
using IdentityServer.Application.Common.Interfaces.Repositories;
using IdentityServer.Infrastructure.Data.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionstring = configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionstring);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<IPermissionService, PermissionService>();

        services.AddSingleton(TimeProvider.System);

        services.AddScoped<ITokenGenerator, EmailTokenGenerator>();

        services.AddOptions<SendGridSettings>()
            .Bind(configuration.GetSection(SendGridSettings.SectionName));

        services.AddScoped<IEmailService, SendGridService>();

        return services;
    }
}
