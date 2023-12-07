using Amazon;
using Microsoft.Extensions.Configuration;

namespace RestTest.Core.Services.ConfigurationManager.Extensions;
public static class ConfigurationExtensions
{
    public static RegionEndpoint GetAwsParameterStoreRegion(this IConfiguration configuration)
    {
        string? configRegionName = configuration
        .GetSection("aws")
        .GetSection("parameterStore")
        .GetValue<string>("region");

        return string.IsNullOrEmpty(configRegionName)
            ? RegionEndpoint.USEast1
            : RegionEndpoint.GetBySystemName(configRegionName);
    }

    public static string GetServiceName(this IConfiguration configuration) => configuration.GetSection("service").GetValue<string>("name")!;
}
