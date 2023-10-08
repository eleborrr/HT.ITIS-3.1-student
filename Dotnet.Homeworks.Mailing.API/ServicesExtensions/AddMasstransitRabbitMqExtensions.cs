using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Mailing.API.Consumers;
using MassTransit;

namespace Dotnet.Homeworks.Mailing.API.ServicesExtensions;

public static class AddMasstransitRabbitMqExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services,
        RabbitMqConfig rabbitConfiguration)
    {
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<EmailConsumer>();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var uri = 
                    new Uri($"amqp://{rabbitConfiguration.Username}:{rabbitConfiguration.Password}@{rabbitConfiguration.Hostname}:{rabbitConfiguration.Port}/");
                configurator.Host(uri);
                configurator.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}