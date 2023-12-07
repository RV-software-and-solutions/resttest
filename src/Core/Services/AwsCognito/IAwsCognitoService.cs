using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using RestTest.Core.Configuration;

namespace RestTest.Core.Services.AwsCognito;
public interface IAwsCognitoService
{
    AmazonCognitoIdentityProviderClient CognitoIdentityProviderClient { get; set; }
    CognitoUserPool CognitoUserPool { get; set; }
    AwsCognitoConfiguration Configuration { get; set; }
}
