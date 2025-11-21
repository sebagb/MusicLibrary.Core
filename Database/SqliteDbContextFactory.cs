using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MusicLibrary.Core.Database;

public class SqliteDbContextFactory
    : IDesignTimeDbContextFactory<MusicLibraryDbContext>
{
    public MusicLibraryDbContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var appSettings = $"appsettings.{env}.json";
        var config = new ConfigurationBuilder()
            .AddJsonFile(appSettings)
            .Build();

        var connection = config.GetSection("Database:ConnectionString").Value;

        var optionsBuilder =
            new DbContextOptionsBuilder<MusicLibraryDbContext>();
        optionsBuilder.UseSqlite($"Data Source={connection}");

        return new MusicLibraryDbContext(optionsBuilder.Options);
    }
}

