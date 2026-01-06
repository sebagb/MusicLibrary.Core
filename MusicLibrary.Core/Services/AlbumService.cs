using MusicLibrary.Core.Models;
using MusicLibrary.Core.Repositories;

namespace MusicLibrary.Core.Services;

public class AlbumService
    (IAlbumRepository database)
    : IAlbumService
{
    private readonly IAlbumRepository database = database;

    public void Create(IEnumerable<Album> albumCollection)
    {
        database.Create(albumCollection);
    }

    public bool Delete(int id)
    {
        return database.Delete(id);
    }

    public IEnumerable<Album> GetAll(GetAllAlbumOptions options)
    {
        var results = database.GetAll();

        results = results.Where(x =>
            options.ArtistFilter == null
            || x.Artist!.Contains(options.ArtistFilter,
                StringComparison.CurrentCultureIgnoreCase));

        results = results.Where(x =>
            options.TitleFilter == null
            || x.Title!.Contains(options.TitleFilter,
                StringComparison.CurrentCultureIgnoreCase));

        foreach (var album in results)
        {
            var countries = database.GetAlbumCountries(album.Id);
            album.Countries.AddRange(countries);

            var genres = database.GetAlbumGenres(album.Id);
            album.Genres.AddRange(genres);

            var styles = database.GetAlbumStyles(album.Id);
            album.Styles.AddRange(styles);
        }

        var ordered = results.OrderByDescending(a => a.AquiredDate);

        return ordered;
    }

    public Album? GetById(int id)
    {
        var album = database.GetById(id);

        if (album == null)
        {
            return null;
        }

        var countries = database.GetAlbumCountries(id);
        album.Countries.AddRange(countries);

        var genres = database.GetAlbumGenres(id);
        album.Genres.AddRange(genres);

        var styles = database.GetAlbumStyles(id);
        album.Styles.AddRange(styles);

        return album;
    }

    public IEnumerable<Album> GetByTitle(string title)
    {
        var results = database.GetAll().AsQueryable();
        var filtered = results;

        if (!string.IsNullOrEmpty(title))
        {
            filtered = results.Where(a =>
                a!.Title!.ToUpper()
                    .Contains(title.ToUpper()));
        }

        foreach (var album in filtered)
        {
            var countries = database.GetAlbumCountries(album.Id);
            album.Countries.AddRange(countries);

            var genres = database.GetAlbumGenres(album.Id);
            album.Genres.AddRange(genres);

            var styles = database.GetAlbumStyles(album.Id);
            album.Styles.AddRange(styles);
        }

        return filtered;
    }

    public bool Update(Album album)
    {
        var exists = database.AlbumExistsById(album.Id);
        if (!exists)
        {
            return false;
        }
        return database.Update(album);
    }
}