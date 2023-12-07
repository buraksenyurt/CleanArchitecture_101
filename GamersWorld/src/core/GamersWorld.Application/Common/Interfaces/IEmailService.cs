using GamersWorld.Application.Dtos.Email;

namespace GamersWorld.Application.Interfaces;

public interface IEmailService
{
    Task SendAsync(EmailDto emailDto);
}