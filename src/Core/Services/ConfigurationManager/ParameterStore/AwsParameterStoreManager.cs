using System.Collections.Immutable;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Microsoft.Extensions.Options;
using RestTest.Core.Configuration;

namespace RestTest.Core.Services.ConfigurationManager.ParameterStore;
public class AwsParameterStoreManager : IAwsParameterStoreManager
{
    private readonly AwsParameterStoreConfiguration _parameterStoreConfiguration;
    private readonly ServiceConfiguration _serviceConfiguration;
    private ImmutableDictionary<string, string> _parameterStore;

    public AwsParameterStoreManager(IOptions<AwsParameterStoreConfiguration> parameterStoreConfiguration, IOptions<ServiceConfiguration> serviceConfiguration)
    {
        _parameterStoreConfiguration = parameterStoreConfiguration.Value;
        _serviceConfiguration = serviceConfiguration.Value;
        InitConfigurationManager().Wait();
    }

    public async Task InitConfigurationManager()
    {
        string parameterStorePath = AwsParameterStorePath;

        AmazonSimpleSystemsManagementClient client = new(_parameterStoreConfiguration.AccessKeyId, _parameterStoreConfiguration.SecretAccessKey, _parameterStoreConfiguration.RegionEndpoint);
        GetParametersByPathResponse allParameters = await client.GetParametersByPathAsync(new GetParametersByPathRequest()
        {
            Path = parameterStorePath,
        });

        _parameterStore = allParameters.Parameters.ToImmutableDictionary(parameter => parameter.Name.Remove(0, parameterStorePath.Length), parameter => parameter.Value);
    }

    public string RetriveConfigValue(string key) => _parameterStore[key];

    public string? this[string key]
    {
        get => FindParameterValue(key);
    }

    private string? FindParameterValue(string key)
    {
        if (_parameterStore.TryGetValue(key, out var value))
        {
            return value;
        }
        return string.Empty;
    }

    private string AwsParameterStorePath =>
        $"/{_serviceConfiguration.Name.ToLower()}/{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!.ToLower()}/";
}
