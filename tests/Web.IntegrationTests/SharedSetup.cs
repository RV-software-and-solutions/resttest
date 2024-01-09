using Microsoft.Extensions.DependencyInjection;
using Web.IntegrationTests.Core;

[SetUpFixture]
#pragma warning disable S3903 // Types should be defined in named namespaces
public class SharedSetup
#pragma warning restore S3903 // Types should be defined in named namespaces
{
    private static readonly CustomWebApplicationFactory Factory = new();
    private static IServiceProvider ServiceProvider => Factory.Services;
    private static IServiceScope? _childScope;
    public static readonly DatabaseSetup DatabaseSetup = new();
    public static HttpClient Client { get; private set; } = default!;

    public static string? CurrentUserId;

    [OneTimeSetUp]
    public async Task RunBeforeTests()
    {
        await DatabaseSetup.InitializeDbContainer();
        Client = Factory.CreateClient();
        await DatabaseSetup.InitializeRespawner();
    }

    [OneTimeTearDown]
    public async Task RunAfterTests()
    {
        _childScope?.Dispose();
        await DatabaseSetup.CleanUp();
        await Factory.DisposeAsync();
        Client.Dispose();
    }

    public static T GetRequiredService<T>() where T : notnull
    {
        _childScope?.Dispose();
        _childScope = ServiceProvider.CreateScope();
        var childScope = _childScope.ServiceProvider;
        return childScope.GetRequiredService<T>();
    }
}
