using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestTest.Core.Attributes;
using RestTest.Core.Configuration;
using RestTest.Core.Services.AwsCognito;
using RestTest.Core.Services.AwsDynamo;
using RestTest.Core.Services.ConfigurationManager.ParameterStore;
using RestTest.Core.Services.Graph;
using RestTest.Core.Services.ResourceManager.Core;
using RestTest.Core.Services.ResourceManager.FileManager;
using RestTest.Core.Services.ResourceManager.S3;

namespace RestTest.Core;
public static class ConfigureServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.LoadServiceConfiguration(configuration);
        services.LoadAwsDependencies(configuration);

        services.AddTransient<IResourceManager<FileResource>, FileResourceManager>();
        services.AddTransient<IResourceManager<S3Resource>, S3ResourceManager>();

        services.AddSingleton<IAwsParameterStoreManager, AwsParameterStoreManager>();
        services.AddSingleton(typeof(IGraphService<,>), typeof(GraphService<,>));

        return services;
    }

    public static IServiceCollection LoadServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServiceConfiguration>(serviceConfiguration => configuration.GetSection("service")
            .GetSection(typeof(ServiceConfiguration).GetAttribute<ConfigurationAttribute>().Key)
            .Bind(serviceConfiguration));

        return services;
    }

    public static IServiceCollection LoadAwsDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection awsConfigurationSection = configuration.GetSection("aws");
        string defaultAwsRegion = awsConfigurationSection.GetValue<string>("defaultRegion") ?? string.Empty;

        #region AWS S3

        services.Configure<S3Configuration>(awsConfiguration =>
        {
            awsConfigurationSection
            .GetSection(typeof(S3Configuration).GetAttribute<ConfigurationAttribute>().Key)
            .Bind(awsConfiguration);

            awsConfiguration.SetAwsRegion(defaultAwsRegion);
        });

        #endregion

        #region AWS Parameter store

        services.Configure<AwsParameterStoreConfiguration>(awsConfiguration =>
        {
            awsConfigurationSection
                .GetSection(typeof(AwsParameterStoreConfiguration).GetAttribute<ConfigurationAttribute>().Key)
                .Bind(awsConfiguration);

            awsConfiguration.SetAwsRegion(defaultAwsRegion);
        });

        #endregion

        #region AWS cognito

        services.Configure<AwsCognitoConfiguration>(awsConfiguration =>
        {
            awsConfigurationSection
            .GetSection(typeof(AwsCognitoConfiguration).GetAttribute<ConfigurationAttribute>().Key)
            .Bind(awsConfiguration);

            awsConfiguration.SetAwsRegion(defaultAwsRegion);
        });

        services.AddSingleton<IAwsCognitoService, AwsCognitoService>();

        #endregion

        #region AWS DynamoDb

        services.Configure<AwsDynamoDbConfiguration>(awsConfiguration =>
        {
            awsConfigurationSection
            .GetSection(typeof(AwsDynamoDbConfiguration).GetAttribute<ConfigurationAttribute>().Key)
            .Bind(awsConfiguration);

            awsConfiguration.SetAwsRegion(defaultAwsRegion);
        });

        services.AddSingleton<IAwsDynamoService, AwsDynamoService>();

        #endregion

        return services;
    }
}
