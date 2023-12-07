using Amazon;

namespace RestTest.Core.Configuration;
public abstract class AbstractBaseAwsConfiguration
{
    public string? AccessKeyId { get; set; }
    public string? SecretAccessKey { get; set; }

    public required string Region { get; set; } = "us-east-1";

    public RegionEndpoint RegionEndpoint => RegionEndpoint.GetBySystemName(Region);
}
