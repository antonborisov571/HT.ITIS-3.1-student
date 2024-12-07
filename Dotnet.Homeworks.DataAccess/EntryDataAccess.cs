using Dotnet.Homeworks.DataAccess.Repositories;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.DataAccess;

public static class EntryDataAccess
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        return services.AddScoped<IProductRepository, ProductRepository>();
    }
}