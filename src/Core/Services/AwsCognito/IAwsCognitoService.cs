using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using RestTest.Core.Configuration;

namespace RestTest.Core.Services.AwsCognito;
public interface IAwsCognitoService
{
    AmazonCognitoIdentityProviderClient CognitoIdentityProviderClient { get; set; }
    CognitoUserPool CognitoUserPool { get; set; }
    AwsCognitoConfiguration Configuration { get; set; }

    Task<UserType> FindUsersByEmailAddress(string emailAddress);

    Task<CodeDeliveryDetailsType?> ResendConfirmationEmail(UserType cognitoUser, CancellationToken cancellationToken);
}
