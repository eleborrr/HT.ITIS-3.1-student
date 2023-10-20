using Dotnet.Homeworks.MainProject.Configuration;
using MassTransit;

namespace Dotnet.Homeworks.MainProject.ServicesExtensions.Masstransit;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitConfiguration = new RabbitMqConfig
        {
            Hostname = configuration["MessageBroker:Hostname"],
            Password = configuration["MessageBroker:Password"],
            Username = configuration["MessageBroker:Username"],
            Port = configuration["MessageBroker:Port"]
        };
        
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                Console.WriteLine("--------------->");
                Console.WriteLine(rabbitConfiguration.Username);
                Console.WriteLine(rabbitConfiguration.Password);
                Console.WriteLine(rabbitConfiguration.Hostname);
                Console.WriteLine(rabbitConfiguration.Port);
                Console.WriteLine("--------------->");
                var uri =
                    $"amqp://{rabbitConfiguration.Username}:{rabbitConfiguration.Password}@{rabbitConfiguration.Hostname}:{rabbitConfiguration.Port}";
                configurator.Host(uri);
                configurator.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}