using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using RestTest.Core.Dtos.Auth;
using RestTest.Core.Services.AwsCognito;

namespace RestTest.Application.Auth.Commands.LoginUser;
public record LoginUserCommand(string EmailAddress, string Password) : IRequest<AuthResponseDto>;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponseDto>
{
    private readonly IAwsCognitoService _awsCognitoService;

    public LoginUserCommandHandler(IAwsCognitoService awsCognitoService)
    {
        _awsCognitoService = awsCognitoService;
    }

    public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            CognitoUser user = new(
                    request.EmailAddress,
                    _awsCognitoService.Configuration.UserPoolClientId,
                    _awsCognitoService.CognitoUserPool,
                    _awsCognitoService.CognitoIdentityProviderClient);

            InitiateSrpAuthRequest authRequest = new()
            {
                Password = request.Password
            };

            AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest);
            AuthenticationResultType result = authResponse.AuthenticationResult;

            return new AuthResponseDto
            {
                EmailAddress = user.UserID,
                UserId = user.UserID,
                Username = user.Username,
                IsSuccess = true,
                Token = new TokenDto(result.IdToken, result.AccessToken, result.ExpiresIn, result.RefreshToken)
            };
        }
        catch (UserNotConfirmedException)
        {
            await UserNotConfirmedAccount(request.EmailAddress, cancellationToken);
        }
        catch (UserNotFoundException)
        {
            // Occurs if the provided emailAddress 
            // doesn't exist in the UserPool
            return new AuthResponseDto
            {
                IsSuccess = false,
                Message = "EmailAddress not found.",
                Status = CognitoStatusCodeEnums.USER_NOTFOUND
            };
        }
        catch (NotAuthorizedException)
        {
            return new AuthResponseDto
            {
                IsSuccess = false,
                Message = "Incorrect username or password"
            };
        }

        return new AuthResponseDto
        {
            IsSuccess = false,
            Message = "Incorrect username or password"
        };
    }

    private async Task<AuthResponseDto> UserNotConfirmedAccount(string emailAddress, CancellationToken cancellationToken)
    {
        UserType filteredUser = await _awsCognitoService.FindUsersByEmailAddress(emailAddress);

        CodeDeliveryDetailsType? resendCodeResponse = await _awsCognitoService.ResendConfirmationEmail(filteredUser, cancellationToken);

        if (resendCodeResponse is not null)
        {
            return new AuthResponseDto
            {
                IsSuccess = false,
                Message = $"Confirmation Code sent to {emailAddress} via {resendCodeResponse.DeliveryMedium.Value}",
                Status = CognitoStatusCodeEnums.USER_UNCONFIRMED,
                UserId = filteredUser.Username,
            };
        }

        return new AuthResponseDto
        {
            IsSuccess = false,
            Message = $"Resend Confirmation Code faild to {emailAddress}",
            Status = CognitoStatusCodeEnums.API_ERROR,
            UserId = filteredUser.Username,
        };
    }
}
