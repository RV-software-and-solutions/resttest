using Amazon;

namespace RestTest.Core.Configuration;
public abstract class AbstractBaseAwsConfiguration
{
    public required string Region { get; set; } = "us-east-1";

    public RegionEndpoint RegionEndpoint => RegionEndpoint.GetBySystemName(Region);
}
