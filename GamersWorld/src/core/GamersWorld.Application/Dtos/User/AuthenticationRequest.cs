using System.ComponentModel.DataAnnotations;

namespace GamersWorld.Application.Dtos.User;

public class AuthenticationRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}