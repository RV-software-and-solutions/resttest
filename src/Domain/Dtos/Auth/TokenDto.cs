namespace RestTest.Domain.Dtos.Auth;
public record class TokenDto(string IdToken, string AccessToken, int ExpiresIn, string RefreshToken);
