using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TestInitProject.Application.Common.Interfaces.Auth;
using TestInitProject.Web.Infrastructure;

namespace TestInitProject.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, CurrentUser>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddControllers();

        return services;
    }
}
