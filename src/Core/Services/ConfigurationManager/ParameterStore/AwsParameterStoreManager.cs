﻿using System.Collections.Immutable;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Microsoft.Extensions.Configuration;
using RestTest.Core.Services.ConfigurationManager.Extensions;

namespace RestTest.Core.Services.ConfigurationManager.ParameterStore;
public class AwsParameterStoreManager : IAwsParameterStoreManager
{
    private readonly IConfiguration _configuration;
    private ImmutableDictionary<string, string> _parameterStore;

    public AwsParameterStoreManager(IConfiguration configuration)
    {
        _configuration = configuration;
        InitConfigurationManager().Wait();
    }

    public async Task InitConfigurationManager()
    {
        string parameterStorePath = AwsParameterStorePath;

        AmazonSimpleSystemsManagementClient client = new(_configuration.GetAwsParameterStoreRegion());
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
        $"/{_configuration.GetServiceName().ToLower()}/{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!.ToLower()}/";
}
