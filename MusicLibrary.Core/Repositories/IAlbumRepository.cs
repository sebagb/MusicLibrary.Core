using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Repositories;

public interface IAlbumRepository
{
    public void Create(IEnumerable<Album> album);
    public bool Delete(int id);
    public bool AlbumExistsById(int id);
    public IEnumerable<Album> GetAll();
    public Album? GetById(int id);
    public bool Update(Album album);
}