using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Services;

public interface IAlbumService
{
    public void Create(IEnumerable<Album> albumCollection);
    public bool Delete(int id);
    public IEnumerable<Album> GetAll();
    public Album? GetById(int id);
    public IEnumerable<Album> GetByTitle(string title);
    public bool Update(Album album);
}