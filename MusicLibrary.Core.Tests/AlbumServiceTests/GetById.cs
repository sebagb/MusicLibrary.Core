
using MusicLibrary.Core.Services;

namespace MusicLibrary.Core.Tests.AlbumServiceTests;

public class GetById
{
    [Fact]
    public void AlbumIsNullWhenNotFound()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums();
        var service = new AlbumService(repo);

        var album = service.GetById(1489091123);

        Assert.Null(album);
    }

    [Fact]
    public void AlbumIsNotNullWhenFound()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums();
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        Assert.NotNull(album);
    }
}