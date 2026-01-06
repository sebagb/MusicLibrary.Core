
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

    [Fact]
    public void CountriesCollectionIsEmptyIfAlbumHasNoCountries()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums();
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        var albumCountries = album!.Countries;
        Assert.Empty(albumCountries);
    }

    [Fact]
    public void CountriesAreLoaded()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums()
            .WithCountries();
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        var albumCountries = album!.Countries;
        Assert.NotEmpty(albumCountries);
    }

    [Fact]
    public void GenresCollectionIsEmptyIfAlbumHasNoGenres()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums();
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        var albumGenres = album!.Genres;
        Assert.Empty(albumGenres);
    }

    [Fact]
    public void GenresAreLoaded()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums()
            .WithGenres();
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        var albumGenres = album!.Genres;
        Assert.NotEmpty(albumGenres);
    }

    [Fact]
    public void StylesCollectionIsEmptyIfAlbumHasNoStyles()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums();
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        var albumStyles = album!.Styles;
        Assert.Empty(albumStyles);
    }

    [Fact]
    public void StylesAreLoaded()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums()
            .WithStyles();
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        var albumStyles = album!.Styles;
        Assert.NotEmpty(albumStyles);
    }
}