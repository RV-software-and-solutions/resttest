using Amazon.CognitoIdentityProvider.Model;
using RestTest.Core.Dtos.Auth;
using RestTest.Core.Services.AwsCognito;

namespace RestTest.Application.Auth.Commands.SignUpUser;
public record SignUpUserCommand(string EmailAddress, string Username, string Password) : IRequest<SignUpUserResponseDto>;

public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, SignUpUserResponseDto>
{
    private readonly IAwsCognitoService _awsCognitoService;

    public SignUpUserCommandHandler(IAwsCognitoService awsCognitoService)
    {
        _awsCognitoService = awsCognitoService;
    }

    public async Task<SignUpUserResponseDto> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
    {
        SignUpRequest signUpRequest = new()
        {
            ClientId = _awsCognitoService.Configuration.UserPoolClientId,
            Password = request.Password,
            Username = request.Username,
        };

        signUpRequest.UserAttributes.Add(new AttributeType
        {
            Name = "email",
            Value = request.EmailAddress,
        });

        signUpRequest.UserAttributes.Add(new AttributeType
        {
            Name = "name",
            Value = request.Username,
        });

        try
        {
            SignUpResponse response = await _awsCognitoService.CognitoIdentityProviderClient.SignUpAsync(signUpRequest, cancellationToken);

            return new SignUpUserResponseDto
            {
                UserId = response.UserSub,
                EmailAddress = request.EmailAddress,
                Message = $"Confirmation Code sent to {response.CodeDeliveryDetails.Destination} via {response.CodeDeliveryDetails.DeliveryMedium.Value}",
                IsSuccess = true,
            };
        }
        catch (UsernameExistsException)
        {
            return new SignUpUserResponseDto
            {
                IsSuccess = false,
                Message = "Email address Already Exists"
            };
        }
    }
}
