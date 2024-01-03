namespace RestTest.Core.Dtos.Auth;
public class AuthResponseDto : AbstractAuthResponseDto
{
    public string Username { get; set; }
    public string UserId { get; set; }
    public TokenDto Token { get; set; }
}
