﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RestTest.Application.Common.Interfaces;
using RestTest.Infrastructure.Persistence;

namespace Application.IntegrationTests.Core;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {
            services
                .Remove<ICurrentUserService>()
                .AddTransient(_ => Mock.Of<ICurrentUserService>(s =>
                    s.UserId == BaseTesting.GetCurrentUserId()));

            services
                .Remove<DbContextOptions<ApplicationDbContext>>()

                .AddDbContext<ApplicationDbContext>((IServiceProvider _, DbContextOptionsBuilder options) =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)))

                .ApplyMigrations<ApplicationDbContext>();
        });
    }
}
