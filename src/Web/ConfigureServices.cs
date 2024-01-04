using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NSwag;
using NSwag.Generation.Processors.Security;
using RestTest.Application.Common.Interfaces;
using RestTest.Infrastructure.Persistence;
using RestTest.Web.Services;
using ZymLabs.NSwag.FluentValidation;

namespace RestTest.Web;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddScoped(provider =>
        {
            IEnumerable<FluentValidationRule>? validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
            ILoggerFactory? loggerFactory = provider.GetService<ILoggerFactory>();

            return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        });

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddOpenApiDocument((configure, serviceProvider) =>
        {
            FluentValidationSchemaProcessor fluentValidationSchemaProcessor = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<FluentValidationSchemaProcessor>();

            // Add the fluent validations schema processor
            configure.SchemaSettings.SchemaProcessors.Add(fluentValidationSchemaProcessor);

            configure.Title = "RestTest API";
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        return services;
    }

    public static IServiceCollection AddAwsCognitoAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        IConfigurationSection awsCognitoConfigSection = configuration.GetSection("aws").GetSection("cognito");
        services
          .AddAuthentication(options =>
          {
              options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
          .AddJwtBearer(options =>
          {
              options.SaveToken = true;
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                  {
                      var json = new HttpClient().GetStringAsync(parameters.ValidIssuer + "/.well-known/jwks.json").Result;
                      return JsonConvert.DeserializeObject<JsonWebKeySet>(json)!.Keys;
                  },
                  ValidateIssuer = true,
                  ValidIssuer = $"https://cognito-idp.{awsCognitoConfigSection["Region"]}.amazonaws.com/{awsCognitoConfigSection["UserPoolId"]}",
                  ValidateLifetime = true,
                  LifetimeValidator = (before, expires, token, param) => expires > DateTime.UtcNow,
                  ValidateAudience = true,
                  ValidAudience = awsCognitoConfigSection["UserPoolClientId"],
              };
          });

        return services;
    }
}
