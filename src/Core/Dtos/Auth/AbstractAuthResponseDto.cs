using RestTest.Core.Services.AwsCognito;

namespace RestTest.Core.Dtos.Auth;
public abstract class AbstractAuthResponseDto
{
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
    public string? EmailAddress { get; set; }
    public CognitoStatusCodeEnums? Status { get; set; }
}
