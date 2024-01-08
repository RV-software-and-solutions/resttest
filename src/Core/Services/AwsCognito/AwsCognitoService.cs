using System.Net;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestTest.Core.Common.Exceptions;
using RestTest.Core.Configuration;
using RestTest.Core.Dtos.Auth;
using RestTest.Core.Dtos.Auth.SignUp;

namespace RestTest.Core.Services.AwsCognito;
public class AwsCognitoService : IAwsCognitoService
{
    public AmazonCognitoIdentityProviderClient CognitoIdentityProviderClient { get; set; }
    public CognitoUserPool CognitoUserPool { get; set; }
    public AwsCognitoConfiguration Configuration { get; set; }
    public readonly ILogger<AwsCognitoService> _logger;

    public AwsCognitoService(IOptions<AwsCognitoConfiguration> awsCognitoConfiguration, ILogger<AwsCognitoService> logger)
    {
        Configuration = awsCognitoConfiguration.Value;
        CognitoIdentityProviderClient = new AmazonCognitoIdentityProviderClient(Configuration.RegionEndpoint);
        CognitoUserPool = new CognitoUserPool(awsCognitoConfiguration.Value.UserPoolId, awsCognitoConfiguration.Value.UserPoolClientId, CognitoIdentityProviderClient);
        _logger = logger;
    }

    public async Task<UserType> FindUsersByEmailAddress(string emailAddress)
    {
        ListUsersRequest listUsersRequest = new()
        {
            UserPoolId = Configuration.UserPoolId,
            Filter = $"email=\"{emailAddress}\""
        };
        ListUsersResponse response = await CognitoIdentityProviderClient.ListUsersAsync(listUsersRequest);
        UserType? cognitoUser = default;
        if (response?.HttpStatusCode != HttpStatusCode.OK && FilterUsersResponse(response.Users, emailAddress, out cognitoUser!))
        {
            _logger.LogError("AWS Cognito Service - failed");
            throw new ServerErrorException("User not found on authenticaiton identity server!");
        }

        return cognitoUser!;
    }

    public async Task<CodeDeliveryDetailsType?> ResendConfirmationEmail(UserType cognitoUser, CancellationToken cancellationToken)
    {
        ResendConfirmationCodeResponse resendCodeResponse = await CognitoIdentityProviderClient.ResendConfirmationCodeAsync(new ResendConfirmationCodeRequest
        {
            ClientId = Configuration.UserPoolClientId,
            Username = cognitoUser.Username
        }, cancellationToken);

        if (resendCodeResponse.HttpStatusCode == HttpStatusCode.OK)
        {
            return resendCodeResponse.CodeDeliveryDetails;
        }

        return default;
    }

    public async Task<SignUpUserResponseDto> SignUpUser(SignUpUserRequestDto signUpDto, CancellationToken cancellationToken)
    {
        SignUpRequest signUpRequest = new()
        {
            ClientId = Configuration.UserPoolClientId,
            Password = signUpDto.Password,
            Username = signUpDto.Username,
        };

        Dictionary<string, string> signUpRequestAttributes = new()
        {
            { "email", signUpDto.EmailAddress },
            { "name", signUpDto.Username },
        };

        signUpRequest.UserAttributes.AddRange(signUpRequestAttributes.Select(attribute => new AttributeType() { Name = attribute.Key, Value = attribute.Value }));

        try
        {
            SignUpResponse response = await CognitoIdentityProviderClient.SignUpAsync(signUpRequest, cancellationToken);

            return new SignUpUserResponseDto
            {
                UserId = response.UserSub,
                EmailAddress = signUpDto.EmailAddress,
                Message = $"Confirmation Code sent to {response.CodeDeliveryDetails.Destination} via {response.CodeDeliveryDetails.DeliveryMedium.Value}",
                IsSuccess = true,
            };
        }
        catch (UsernameExistsException)
        {
            return new SignUpUserResponseDto
            {
                IsSuccess = false,
                Message = "Email address Already Exists",
            };
        }
    }

    private static bool FilterUsersResponse(List<UserType> users, string emailAddress, out UserType? targetUser)
    {
        UserType? filteredUser = users.Find(x => x.Attributes.Exists(x => x.Name == "email" && x.Value == emailAddress));
        if (filteredUser != null)
        {
            targetUser = filteredUser;
            return true;
        }

        targetUser = null;

        return false;
    }
}
