using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RestTest.Infrastructure.Identity;
using RestTest.Infrastructure.Persistence;

namespace Web.IntegrationTests.Core;

[TestFixture]
public abstract class BaseTesting
{
    private static DatabaseSetup DatabaseSetup => SharedSetup.DatabaseSetup;
    protected static HttpClient Client => SharedSetup.Client;

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        var mediator = GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        var mediator = GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static string? GetCurrentUserId()
    {
        return SharedSetup.CurrentUserId;
    }

    public static async Task<string> RunAsDefaultUserAsync()
    {
        return await RunAsUserAsync("test@local", "Testing1234!", Array.Empty<string>());
    }

    public static async Task<string> RunAsAdministratorAsync()
    {
        return await RunAsUserAsync("administrator@local", "Administrator1234!", new[] { "Administrator" });
    }

    public static async Task<string> RunAsUserAsync(string userName, string password, string[] roles)
    {
        var userManager = GetRequiredService<UserManager<ApplicationUser>>();

        var user = new ApplicationUser { UserName = userName, Email = userName };

        var result = await userManager.CreateAsync(user, password);

        if (roles.Any())
        {
            var roleManager = GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            await userManager.AddToRolesAsync(user, roles);
        }

        if (result.Succeeded)
        {
            SharedSetup.CurrentUserId = user.Id;

            return SharedSetup.CurrentUserId;
        }

        var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

        throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        var context = GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        var context = GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        var context = GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    protected static T GetRequiredService<T>() where T : notnull
    {
        return SharedSetup.GetRequiredService<T>();
    }

    [OneTimeSetUp]
    protected virtual async Task RunBeforeTests()
    {
        await ResetDatabase();
    }

    private static async Task ResetDatabase()
    {
        await DatabaseSetup.Respawner.ResetAsync(DatabaseSetup.DbConnection);

        ((NpgsqlConnection)DatabaseSetup.DbConnection).ReloadTypes();

        var initializer = GetRequiredService<ApplicationDbContextInitialiser>();
        await initializer.TrySeedAsync();
    }
}
