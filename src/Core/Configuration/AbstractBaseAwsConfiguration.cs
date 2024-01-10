using Amazon;

namespace RestTest.Core.Configuration;
public abstract class AbstractBaseAwsConfiguration
{
    public string? Region { get; set; }

    public RegionEndpoint RegionEndpoint => RegionEndpoint.GetBySystemName(Region);

    public void SetAwsRegion(string? defaultAwsRegion)
    {
        if (string.IsNullOrEmpty(Region))
        {
            Region = defaultAwsRegion ?? throw new ArgumentException("aws:defaultRegion or region per aws service must be specified!");
        }
    }
}
