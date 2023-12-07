namespace RestTest.Domain.Dtos.Auth;
public class ChangeUserPasswordResponseDto : AbstractAuthResponseDto
{
    public required string UserId { get; set; }
}
