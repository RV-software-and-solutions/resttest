using Microsoft.Extensions.DependencyInjection;
using Web.IntegrationTests.Core;

namespace Web.IntegrationTests;

// This class should not have namespace, so it can be applied to all tests. That is how NUnit works
[SetUpFixture]
// ReSharper disable once CheckNamespace
public class SharedSetup
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
