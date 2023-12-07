using RestTest.Core.Attributes;

namespace RestTest.Core.Configuration;

[Configuration(Key = "cognito")]
public class AwsCognitoConfiguration : AbstractBaseAwsConfiguration
{
    public required string UserPoolClientId { get; set; }
    public required string UserPoolId { get; set; }
}
