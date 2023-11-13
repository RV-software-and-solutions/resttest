using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestTest.Core.Attributes;
using RestTest.Core.Services.ConfigurationManager.ParameterStore;
using RestTest.Core.Services.ResourceManager.Configuration;
using RestTest.Core.Services.ResourceManager.Core;
using RestTest.Core.Services.ResourceManager.FileManager;
using RestTest.Core.Services.ResourceManager.S3;

namespace RestTest.Core;
public static class ConfigureServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuraiton)
    {
        services.Configure<S3Configuration>(s3Configuration => configuraiton
            .GetSection("aws")
            .GetSection(typeof(S3Configuration).GetAttribute<ConfigurationAttribute>().Key)
            .Bind(s3Configuration));

        services.AddTransient<IResourceManager<FileResource>, FileResourceManager>();
        services.AddTransient<IResourceManager<S3Resource>, S3ResourceManager>();

        services.AddSingleton<IAwsParameterStoreManager, AwsParameterStoreManager>();

        return services;
    }
}
