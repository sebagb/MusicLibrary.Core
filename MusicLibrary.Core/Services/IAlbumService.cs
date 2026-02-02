using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Services;

public interface IAlbumService
{
    public void Create(IEnumerable<Album> albumCollection);
    public bool Delete(int id);
    public IEnumerable<Album> GetAll(GetAllAlbumOptions options);
    public Album? GetById(int id);
    public Album Update(Album album);
    public Album? UpdateWithDiscogsResults(
        int albumId,
        DiscogsSelectedResults results);
}