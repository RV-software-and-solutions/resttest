using RestTest.Core.Attributes;

namespace RestTest.Core.Configuration;

[Configuration(Key = "parameterStore")]
public class AwsParameterStoreConfiguration : AbstractBaseAwsConfiguration
{
    public List<string>? Keys { get; set; }
}
