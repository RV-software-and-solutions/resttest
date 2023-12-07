using System.Net;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using RestTest.Core.Services.AwsCognito;
using RestTest.Domain.Dtos.Auth;
using RestTest.Domain.Enums.Auth;

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
            ListUsersResponse listUsersResponse = await FindUsersByEmailAddress(request.EmailAddress);

            if (listUsersResponse is not null && listUsersResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                List<UserType> users = listUsersResponse.Users;
                UserType? filteredUser = users.Find(x => x.Attributes.Exists(x => x.Name == "email" && x.Value == request.EmailAddress));

                var resendCodeResponse = await _awsCognitoService.CognitoIdentityProviderClient.ResendConfirmationCodeAsync(new ResendConfirmationCodeRequest
                {
                    ClientId = _awsCognitoService.Configuration.UserPoolClientId,
                    Username = filteredUser.Username
                }, cancellationToken);

                if (resendCodeResponse.HttpStatusCode == HttpStatusCode.OK)
                {
                    return new AuthResponseDto
                    {
                        IsSuccess = false,
                        Message = $"Confirmation Code sent to {resendCodeResponse.CodeDeliveryDetails.Destination} via {resendCodeResponse.CodeDeliveryDetails.DeliveryMedium.Value}",
                        Status = CognitoStatusCodes.USER_UNCONFIRMED,
                        UserId = filteredUser.Username,
                    };
                }

                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = $"Resend Confirmation Code Response: {resendCodeResponse.HttpStatusCode}",
                    Status = CognitoStatusCodes.API_ERROR,
                    UserId = filteredUser.Username,
                };
            }
        }
        catch (UserNotFoundException)
        {
            // Occurs if the provided emailAddress 
            // doesn't exist in the UserPool
            return new AuthResponseDto
            {
                IsSuccess = false,
                Message = "EmailAddress not found.",
                Status = CognitoStatusCodes.USER_NOTFOUND
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

    private async Task<ListUsersResponse> FindUsersByEmailAddress(string emailAddress)
    {
        ListUsersRequest listUsersRequest = new()
        {
            UserPoolId = _awsCognitoService.Configuration.UserPoolId,
            Filter = $"email=\"{emailAddress}\""
        };
        return await _awsCognitoService.CognitoIdentityProviderClient.ListUsersAsync(listUsersRequest);
    }
}
