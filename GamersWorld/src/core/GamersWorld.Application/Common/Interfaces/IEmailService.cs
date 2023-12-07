using GamersWorld.Application.Dtos.Email;

namespace GamersWorld.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendAsync(EmailDto emailDto);
}