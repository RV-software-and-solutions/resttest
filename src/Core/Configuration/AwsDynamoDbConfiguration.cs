using RestTest.Core.Attributes;

namespace RestTest.Core.Configuration;

[Configuration(Key = "dynamo")]
public class AwsDynamoDbConfiguration : AbstractBaseAwsConfiguration
{
}
