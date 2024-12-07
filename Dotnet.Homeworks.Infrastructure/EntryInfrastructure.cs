using Dotnet.Homeworks.Infrastructure.UnitOfWork;

namespace Dotnet.Homeworks.Infrastructure;

public static class EntryInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }
}