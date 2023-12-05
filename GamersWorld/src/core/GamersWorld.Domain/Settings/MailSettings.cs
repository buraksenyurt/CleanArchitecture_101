namespace GamersWorld.Domain.Settings;

public class MailSettings
{
    public string From { get; set; } = "info@gamersworld";
    public string SmtpHost { get; set; } = "smtphost";
    public int SmtpPort { get; set; }
    public string SmtpUser { get; set; } = "admin";
    public string SmtpPass { get; set; } = "admin";
    public string DisplayName { get; set; } = "Gamers World";
}