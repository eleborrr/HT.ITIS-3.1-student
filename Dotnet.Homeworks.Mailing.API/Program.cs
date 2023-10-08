using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.Mailing.API.ServicesExtensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));
builder.Services.AddScoped<IMailingService, MailingService>();

var rabbitMqConfig = new RabbitMqConfig
{
    Hostname = builder.Configuration["MessageBroker:Hostname"],
    Password = builder.Configuration["MessageBroker:Password"],
    Username = builder.Configuration["MessageBroker:Username"],
    Port = builder.Configuration["MessageBroker:Port"]
};


builder.Services.AddMasstransitRabbitMq(rabbitMqConfig);


var app = builder.Build();

app.Run();