using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using RestTest.Core.Services.AwsCognito;
using RestTest.Domain.Dtos.Auth;

namespace RestTest.Application.Auth.Commands.ChangeUserPassword;
public record ChangeUserPasswordCommand(string CurrentPassword, string EmailAddress, string NewPassword) : IRequest<ChangeUserPasswordResponseDto>;

public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, ChangeUserPasswordResponseDto>
{
    private readonly IAwsCognitoService _awsCognitoService;

    public ChangeUserPasswordCommandHandler(IAwsCognitoService awsCognitoService)
    {
        _awsCognitoService = awsCognitoService;
    }

    public async Task<ChangeUserPasswordResponseDto> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        Tuple<CognitoUser, AuthenticationResultType> tokenResponse = await AuthenticateUserAsync(request.EmailAddress, request.CurrentPassword);

        ChangePasswordRequest changePasswordRequest = new()
        {
            AccessToken = tokenResponse.Item2.AccessToken,
            PreviousPassword = request.CurrentPassword,
            ProposedPassword = request.NewPassword,
        };
        ChangePasswordResponse response = await _awsCognitoService.CognitoIdentityProviderClient.ChangePasswordAsync(changePasswordRequest, cancellationToken);
        return new ChangeUserPasswordResponseDto { UserId = tokenResponse.Item1.Username, Message = "Password Changed", IsSuccess = true };
    }

    private async Task<Tuple<CognitoUser, AuthenticationResultType>> AuthenticateUserAsync(string emailAddress, string password)
    {
        CognitoUser user = new(emailAddress, _awsCognitoService.Configuration.UserPoolClientId, _awsCognitoService.CognitoUserPool, _awsCognitoService.CognitoIdentityProviderClient);
        InitiateSrpAuthRequest authRequest = new()
        {
            Password = password
        };

        AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest);
        AuthenticationResultType result = authResponse.AuthenticationResult;
        return new Tuple<CognitoUser, AuthenticationResultType>(user, result);
    }
}
