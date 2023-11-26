using RestTest.Domain.Enums.Auth;

namespace RestTest.Domain.Dtos.Auth;
public abstract class AbstractAuthResponseDto
{
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
    public string? EmailAddress { get; set; }
    public CognitoStatusCodes? Status { get; set; }

}
