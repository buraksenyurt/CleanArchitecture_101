namespace GamersWorld.Application.Dtos.User;

public class AuthenticationResponse(Domain.Entities.User user, string token)
{
    public int UserId { get; set; } = user.UserId;
    public string Fullname { get; set; } = $"{user.Name} {user.LastName}";
    public string Token { get; set; } = token;
    public string Email { get; set; } = user.Email;
}