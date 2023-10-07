using System.Data.Common;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;

namespace Application.IntegrationTests;

public class DatabaseSetup
{
    public Respawner Respawner { get; set; } = default!;
    public static DbConnection DbConnection { get; set; } = default!;

    private readonly PostgreSqlContainer _dbContainer;

    public DatabaseSetup()
    {
        _dbContainer =
            new PostgreSqlBuilder()
                .WithDatabase("postgres-integration-tests")
                .WithUsername("postgres")
                .WithPassword("root123")
                .WithPortBinding("5454", "5432")
                .WithImage("postgres:14.7-alpine")
                .Build();
    }

    public async Task CleanUp()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
    }

    public async Task InitializeDbContainer()
    {
        await _dbContainer.StartAsync();
        DbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
    }

    public async Task InitializeRespawner()
    {
        await DbConnection.OpenAsync();

        Respawner = await Respawner.CreateAsync(DbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new string[] { "public" },
            WithReseed = true,
        });
    }
}
