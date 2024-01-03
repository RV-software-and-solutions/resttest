namespace RestTest.Core.Dtos.Auth;
public class ChangeUserPasswordResponseDto : AbstractAuthResponseDto
{
    public required string UserId { get; set; }
}
