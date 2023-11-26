using RestTest.Domain.Dtos.Auth;

namespace RestTest.Web.Models.Views.Auth;

public class TokenView
{
    public string IdToken { get; }
    public string AccessToken { get; }
    public int ExpiresIn { get; }
    public string RefreshToken { get; }

    public TokenView(TokenDto dto)
    {
        IdToken = dto.IdToken;
        AccessToken = dto.AccessToken;
        ExpiresIn = dto.ExpiresIn;
        RefreshToken = dto.RefreshToken;
    }
}
