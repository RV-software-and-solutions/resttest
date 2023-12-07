namespace RestTest.Web.Extensions;

public static class WebExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(corsOptions =>
        {
            foreach (var c in configuration.ReadCorsPolicies())
            {
                corsOptions.AddPolicy(c.Name, builder =>
                {
                    builder
                        .WithOrigins(c.AllowedOrigins.ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            }
        });

        return services;
    }

    public static WebApplication SetCorsPolicy(this WebApplication application)
    {
        foreach (var c in application.Configuration.ReadCorsPolicies())
        {
            application.UseCors(c.Name);
        }

        return application;
    }

    public static List<CorsPolicyConfig> ReadCorsPolicies(this IConfiguration configuration)
        => configuration
            .GetSection("service")
            .GetSection("corsPolicies")
            .Get<List<CorsPolicyConfig>>()!;
}
