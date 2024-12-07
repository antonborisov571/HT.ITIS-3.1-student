using Dotnet.Homeworks.MainProject.Helpers;

namespace Dotnet.Homeworks.MainProject.ServicesExtensions.MediatR;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        return services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssemblies(Features.Helpers.AssemblyReference.Assembly));
    }
}