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

    public IEnumerable<Album> GetAll()
    {
        var results = database.GetAll();

        var ordered = results.OrderByDescending(a => a.AquiredDate);

        return ordered;
    }

    public Album? GetById(int id)
    {
        return database.GetById(id);
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