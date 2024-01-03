using Amazon.CognitoIdentityProvider.Model;
using RestTest.Core.Dtos.Auth;
using RestTest.Core.Services.AwsCognito;

namespace RestTest.Application.Auth.Commands.ConfirmUserSignUp;

public record ConfirmUserSignUpCommand(string ConfirmationCode, string EmailAddress, string UserId) : IRequest<SignUpUserResponseDto>;

public class ConfirmUserSignUpCommandHandler : IRequestHandler<ConfirmUserSignUpCommand, SignUpUserResponseDto>
{
    private readonly IAwsCognitoService _awsCognitoService;

    public ConfirmUserSignUpCommandHandler(IAwsCognitoService awsCognitoService)
    {
        _awsCognitoService = awsCognitoService;
    }

    public async Task<SignUpUserResponseDto> Handle(ConfirmUserSignUpCommand request, CancellationToken cancellationToken)
    {
        ConfirmSignUpRequest confirmSignUpRequest = new()
        {
            ClientId = _awsCognitoService.Configuration.UserPoolClientId,
            ConfirmationCode = request.ConfirmationCode,
            Username = request.EmailAddress,
        };
        var response = await _awsCognitoService.CognitoIdentityProviderClient.ConfirmSignUpAsync(confirmSignUpRequest);

        // add to default users group
        //var addUserToGroupRequest = new AdminAddUserToGroupRequest
        //{
        //    UserPoolId = _cloudConfig.UserPoolId,
        //    Username = model.UserId,
        //    GroupName = "-users-group"
        //};
        //var addUserToGroupResponse = await _provider.AdminAddUserToGroupAsync(addUserToGroupRequest);

        return new SignUpUserResponseDto
        {
            EmailAddress = request.EmailAddress,
            UserId = request.UserId,
            Message = "User Confirmed",
            IsSuccess = true,
        };
    }
}
