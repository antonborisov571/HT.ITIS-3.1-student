using Dotnet.Homeworks.MainProject.Configuration;
using MassTransit;

namespace Dotnet.Homeworks.MainProject.ServicesExtensions.Masstransit;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services,
        RabbitMqConfig rabbitConfiguration)
    {
        return services.AddMassTransit(configurator =>
        {
            configurator.AddConsumers(typeof(Program).Assembly);
            configurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitConfiguration.Hostname, "/", h =>
                {
                    h.Username(rabbitConfiguration.Username);
                    h.Password(rabbitConfiguration.Password);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}