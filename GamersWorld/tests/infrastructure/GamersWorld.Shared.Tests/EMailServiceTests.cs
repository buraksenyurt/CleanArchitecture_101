using GamersWorld.Application.Common.Exceptions;
using GamersWorld.Application.Dtos.Email;
using GamersWorld.Domain.Settings;
using GamersWorld.Shared.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Shared.Tests;

public class EMailServiceTests
{
    private readonly Mock<ILogger<EMailService>> _mockLogger;
    private readonly IOptions<MailSettings> _mockMailSettings;
    public EMailServiceTests()
    {
        _mockLogger = new Mock<ILogger<EMailService>>();
        _mockMailSettings = Options.Create(new MailSettings
        {
            From = "mario@gamersworld.com",
            DisplayName = "Gamers World Magazine",
            SmtpHost = "localhost",
            SmtpPort = 25,
            SmtpUser = "admin",
            SmtpPass = "1234"
        });
    }

    [Fact]
    public async Task SendAsync_WithValidEmail_DoesNotThrow()
    {
        // Arrange
        var emailService = new EMailService(_mockLogger.Object, _mockMailSettings);
        var emailDto = new EmailDto
        {
            From = "info@gamersworld.com",
            To = "john.doe@amigafuns.com",
            Subject = "New Era Begins",
            Body = "A New Era Begins. Please follow the White Rabbit"
        };

        // Act & Assert
        await Assert.ThrowsAsync<MailSendException>(() => emailService.SendAsync(emailDto));
    }
}