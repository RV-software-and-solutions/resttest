using RestTest.Core.Attributes;

namespace RestTest.Core.Configuration;

[Configuration(Key = "s3")]
public class S3Configuration : AbstractBaseAwsConfiguration
{
    public required string BucketName { get; set; }
    public int DefaultTimeoutInMinutes { get; set; } = 10;
    public int ClientMaxErrorRetry { get; set; } = 5;
}
