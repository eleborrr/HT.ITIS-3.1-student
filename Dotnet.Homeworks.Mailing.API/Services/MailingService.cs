using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Mailing.API.Dto;
using Dotnet.Homeworks.Shared.Dto;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Dotnet.Homeworks.Mailing.API.Services;

public class MailingService : IMailingService
{
    private readonly EmailConfig _emailConfig;
    private readonly ILogger<MailingService> _logger;

    public MailingService(IOptions<EmailConfig> emailConfig, ILogger<MailingService> logger)
    {
        _logger = logger;
        _emailConfig = emailConfig.Value;
    }

    public async Task<Result> SendEmailAsync(EmailMessage emailDto)
    {
        _logger.Log(LogLevel.Information,  "---------> joined sendemailasync method");
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Testing mailing api", _emailConfig.Email));
        message.To.Add(new MailboxAddress(emailDto.Email, emailDto.Email));
        message.Subject = emailDto.Subject ?? "";
        var bodyBuilder = new BodyBuilder
        {
            TextBody = $"Your message: {emailDto.Content}"
        };
        message.Body = bodyBuilder.ToMessageBody();
        using var client = new SmtpClient();
        _logger.Log(LogLevel.Information,"---------> client configured");
        _logger.Log(LogLevel.Information, message.Body.ToString());
        try
        {
            await client.ConnectAsync(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailConfig.Email, _emailConfig.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            return new Result(true);
        }
        catch (Exception ex)
        {
            return new Result(false, $"Error while sending email: {ex.Message}");
        }
    }
}