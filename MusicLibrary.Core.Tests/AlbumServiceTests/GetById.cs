
using MusicLibrary.Core.Services;

namespace MusicLibrary.Core.Tests.AlbumServiceTests;

public class GetById
{
    [Fact]
    public void AlbumIsNullWhenNotFound()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums()
            .WithCountries();
        var service = new AlbumService(repo);

        var album = service.GetById(1489091123);

        Assert.Null(album);
    }
}