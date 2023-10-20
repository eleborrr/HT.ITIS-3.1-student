using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Mailing.API.Dto;
using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.Shared.MessagingContracts.Email;
using MassTransit;

namespace Dotnet.Homeworks.Mailing.API.Consumers;

public class EmailConsumer : IEmailConsumer
{
    private readonly IMailingService _mailingService;
    private readonly ILogger<EmailConsumer> _logger;

    public EmailConsumer(IMailingService mailingService, ILogger<EmailConsumer> logger)
    {
        _mailingService = mailingService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SendEmail> context)
    {
        var msg = context.Message;

        _logger.Log(LogLevel.Information, $"CONSUMED! {msg.ReceiverEmail}, {msg.Subject}, {msg.Content}");
        var email = new EmailMessage(msg.ReceiverEmail, msg.Subject, msg.Content);
        
        await _mailingService.SendEmailAsync(email);
    }
}