using TestInitProject.Application.Common.Interfaces;
using TestInitProject.Web.Infrastructure;

namespace TestInitProject.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IUser, CurrentUser>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();

        return services;
    }
}
