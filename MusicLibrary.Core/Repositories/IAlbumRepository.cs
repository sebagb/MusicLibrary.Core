using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Repositories;

public interface IAlbumRepository
{
    public void Create(IEnumerable<Album> album);
    public bool Delete(int id);
    public bool AlbumExistsById(int id);
    public IEnumerable<Album> GetAll();
    public IEnumerable<string> GetAlbumCountries(int albumId);
    public IEnumerable<string> GetAlbumGenres(int albumId);
    public IEnumerable<string> GetAlbumStyles(int albumId);
    public Album? GetById(int id);
    public bool Update(Album album);
}