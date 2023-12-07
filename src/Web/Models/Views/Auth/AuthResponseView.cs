using RestTest.Domain.Dtos.Auth;

namespace RestTest.Web.Models.Views.Auth;

public class AuthResponseView
{
    public string Username { get; }
    public string UserId { get; }
    public TokenView Token { get; }

    public AuthResponseView(AuthResponseDto dto)
    {
        UserId = dto.UserId;
        Token = new TokenView(dto.Token);
        Username = dto.Username;
    }
}
