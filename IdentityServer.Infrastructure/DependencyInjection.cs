using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using IdentityServer.Application;
using IdentityServer.Application.Common.Interfaces;
using IdentityServer.Infrastructure;
using IdentityServer.Infrastructure.Data;
using IdentityServer.Infrastructure.Data.Interceptors;

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

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<IPermissionService, PermissionService>();

        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
