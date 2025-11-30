using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicLibrary.Core.ApiClients;
using MusicLibrary.Core.Database;
using MusicLibrary.Core.Repositories;
using MusicLibrary.Core.Services;

namespace MusicLibrary.Core;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection service)
    {
        service.AddScoped<IAlbumService, AlbumService>();
        return service;
    }
    public static IServiceCollection AddDatabase(
        this IServiceCollection service,
        string connectionString)
    {
        service.AddDbContext<MusicLibraryDbContext>(options =>
            options.UseSqlite($"Data Source={connectionString}"));

        service.AddScoped<IAlbumRepository, AlbumRepository>();

        return service;
    }

    public static IServiceCollection ConfigureDiscogsApiClient(
        this IServiceCollection service,
        string discogsApiUri,
        string key,
        string secret)
    {
        service.AddSingleton(_ => new DiscogsAuth(key, secret));

        service.AddHttpClient<DiscogsApiClient>(config =>
        {
            config.BaseAddress = new Uri(discogsApiUri);
            config.DefaultRequestHeaders.Add(
                "User-Agent",
                "MusicLibrary.Net/0.1");
        });

        return service;
    }
}