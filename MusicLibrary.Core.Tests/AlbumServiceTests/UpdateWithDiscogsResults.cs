using MusicLibrary.Core.Models;
using MusicLibrary.Core.Services;

namespace MusicLibrary.Core.Tests.AlbumServiceTests;

public class UpdateWithDiscogsResults
{
    [Fact]
    public void ReturnsNullIfAlbumIsNotInTheRepository()
    {
        var repository = new AlbumRepositoryMock()
            .WithAlbum(id: 2222);
        var service = new AlbumService(repository);

        var albumToUpdate = 33242;
        var album = service.UpdateWithDiscogsResults(
            albumToUpdate,
            new DiscogsSelectedResults());

        Assert.Null(album);
    }
}