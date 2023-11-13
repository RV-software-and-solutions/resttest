using Amazon;
using RestTest.Core.Attributes;

namespace RestTest.Core.Services.ResourceManager.Configuration;

[Configuration(Key = "s3")]
public class S3Configuration
{
    public required string BucketName { get; set; }
    public string Region { get; set; } = "us-east-1";
    public int DefaultTimeoutInMinutes { get; set; } = 10;
    public int ClientMaxErrorRetry { get; set; } = 5;

    public RegionEndpoint RegionEndpoint => RegionEndpoint.GetBySystemName(Region);
}
