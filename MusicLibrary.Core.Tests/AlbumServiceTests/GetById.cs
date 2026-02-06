
using MusicLibrary.Core.Services;

namespace MusicLibrary.Core.Tests.AlbumServiceTests;

public class GetById
{
    [Fact]
    public void AlbumIsNullWhenNotFound()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222);
        var service = new AlbumService(repo);

        var album = service.GetById(1489091123);

        Assert.Null(album);
    }

    [Fact]
    public void AlbumIsNotNullWhenFound()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222);
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        Assert.NotNull(album);
    }

    [Fact]
    public void CountriesCollectionIsEmptyIfAlbumHasNoCountries()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222);
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        var albumCountries = album!.Countries;
        Assert.Empty(albumCountries);
    }

    [Fact]
    public void CountriesAreLoaded()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222)
            .WithAlbumCountry(1111, 30303)
            .WithAlbumCountry(1111, 40404);
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        var albumCountries = album!.Countries;
        Assert.Equal(2, albumCountries.Count);
    }

    [Fact]
    public void GenresCollectionIsEmptyIfAlbumHasNoGenres()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222);
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        var albumGenres = album!.Genres;
        Assert.Empty(albumGenres);
    }

    [Fact]
    public void GenresAreLoaded()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222)
            .WithAlbumGenre(1111, 3232)
            .WithAlbumGenre(1111, 4242);
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        var albumGenres = album!.Genres;
        Assert.Equal(2, albumGenres.Count);
    }

    [Fact]
    public void StylesCollectionIsEmptyIfAlbumHasNoStyles()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222);
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        var albumStyles = album!.Styles;
        Assert.Empty(albumStyles);
    }

    [Fact]
    public void StylesAreLoaded()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222)
            .WithAlbumStyle(1111, 5424)
            .WithAlbumStyle(1111, 5252);
        var service = new AlbumService(repo);

        var album = service.GetById(1111);

        var albumStyles = album!.Styles;
        Assert.Equal(2, albumStyles.Count);
    }
}