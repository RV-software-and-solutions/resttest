using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.Extensions.Options;
using RestTest.Core.Configuration;

namespace RestTest.Core.Services.AwsCognito;
public class AwsCognitoService : IAwsCognitoService
{
    public AmazonCognitoIdentityProviderClient CognitoIdentityProviderClient { get; set; }
    public CognitoUserPool CognitoUserPool { get; set; }
    public AwsCognitoConfiguration Configuration { get; set; }

    public AwsCognitoService(IOptions<AwsCognitoConfiguration> awsCognitoConfiguration)
    {
        Configuration = awsCognitoConfiguration.Value;
        CognitoIdentityProviderClient = new AmazonCognitoIdentityProviderClient(awsCognitoConfiguration.Value.RegionEndpoint);
        CognitoUserPool = new CognitoUserPool(awsCognitoConfiguration.Value.UserPoolId, awsCognitoConfiguration.Value.UserPoolClientId, CognitoIdentityProviderClient);
    }
}
