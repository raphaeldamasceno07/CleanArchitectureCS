using Infrastructure.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using Xunit; // Importante para IAsyncLifetime

namespace IntegrationTests;

public class IntegrationTestBase : IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")   
        .WithPassword("YourSecurePassword123!")
        .Build();

    protected HttpClient Client;
    protected WebApplicationFactory<Program> Factory;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        Factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");
            builder.UseSetting("ConnectionStrings:DefaultConnection", _dbContainer.GetConnectionString());
        });

        // --------------------------------------------------------
        // O PULO DO GATO: Aplicar as tabelas no banco do Testcontainers
        // --------------------------------------------------------
        using var scope = Factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

        Client = Factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        // Importante: Dispose da factory também
        await Factory.DisposeAsync();
        await _dbContainer.StopAsync();
    }
}