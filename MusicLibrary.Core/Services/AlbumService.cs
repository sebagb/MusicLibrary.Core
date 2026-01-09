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

        if (options.SortOrder == GetAllAlbumSortOrder.Unsorted)
        {
            return results.OrderByDescending(a => a.AquiredDate);
        }

        switch (options.SortField)
        {
            case GetAllAlbumSortField.Artist:
                results = options.SortOrder == GetAllAlbumSortOrder.Ascending ?
                    results.OrderBy(x => x.Artist)
                        .ThenBy(x => x.Title)
                    : results.OrderByDescending(x => x.Artist)
                        .ThenByDescending(x => x.Title);
                break;
            case GetAllAlbumSortField.Title:
                results = options.SortOrder == GetAllAlbumSortOrder.Ascending ?
                    results.OrderBy(x => x.Title)
                        .ThenBy(x => x.Artist)
                    : results.OrderByDescending(x => x.Title)
                        .ThenByDescending(x => x.Artist);
                break;
            default:
                break;
        }
        return results;
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