using GamersWorld.Application.Common.Exceptions;
using GamersWorld.Application.Common.Interfaces;
using GamersWorld.Application.Dtos.Email;
using GamersWorld.Domain.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GamersWorld.Shared.Services;

public class EMailService(ILogger<EMailService> logger, IOptions<MailSettings> mailSettings)
        : IEmailService
{
    private readonly MailSettings _mailSettings = mailSettings.Value;
    private readonly ILogger<EMailService> _logger = logger;
    public async Task SendAsync(EmailDto request)
    {
        try
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(request.From ?? _mailSettings.From)
            };
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            var builder = new BodyBuilder { HtmlBody = request.Body };
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw new MailSendException();
        }
    }
}