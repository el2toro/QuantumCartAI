using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Ordering.API;
using Ordering.Infrastructure.Data;
using Respawn;
using System.Data.Common;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;
using MassTransit;

namespace Ordering.Tests.Integration.Configuration;

public class CustomWebApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder("postgres:latest")
        .WithDatabase("test")
        .WithUsername("admin")
        .WithPassword("admin")
        .Build();

    //private readonly RabbitMQContainer _rabbitMqContainer = new RabbitMqBuilder()
    //.WithImage("rabbitmq:3-management")
    //.WithUsername("admin")
    //.WithPassword("admin")
    //.Build();

    private DbConnection _dbConnection = default!;
    private Respawner _respawner = default!;

    public HttpClient HttpClient { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());

        // ✅ OPEN CONNECTION
        await _dbConnection.OpenAsync();

        HttpClient = CreateClient();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OrderingDbContext>();

        await dbContext.Database.MigrateAsync();

        await InitializeRespawnerAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await _dbConnection.DisposeAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //builder.UseSetting("ConnectionStrings:Postgres", _dbContainer.GetConnectionString());

        builder.ConfigureServices(services =>
        {
            var connectionString = _dbContainer.GetConnectionString();

            // Remove existing DbContext config
            services.RemoveAll<DbContextOptions<OrderingDbContext>>();

            // Add correct one
            services.AddDbContext<OrderingDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        });
    }

    private async Task InitializeRespawnerAsync()
    {
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            SchemasToInclude = ["ordering"],
            DbAdapter = DbAdapter.Postgres
        });
    }
}
