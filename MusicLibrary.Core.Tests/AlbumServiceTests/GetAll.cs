using MusicLibrary.Core.Services;

namespace MusicLibrary.Core.Tests.AlbumServiceTests;

public class GetAll
{
    [Fact]
    public void CountriesCollectionIsEmptyIfAlbumHasNoCountries()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums();
        var service = new AlbumService(repo);

        var collection = service.GetAll();

        var firstAlbum = collection.First();
        var albumCountries = firstAlbum.Countries;
        Assert.Empty(albumCountries);
    }

    [Fact]
    public void CountriesAreLoadedForEveryAlbum()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums()
            .WithCountries();
        var service = new AlbumService(repo);

        var collection = service.GetAll();

        var firstAlbum = collection.First();
        var albumCountries = firstAlbum.Countries;
        Assert.NotEmpty(albumCountries);
    }

    [Fact]
    public void GenresCollectionIsEmptyIfAlbumHasNoGenres()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums();
        var service = new AlbumService(repo);

        var collection = service.GetAll();

        var firstAlbum = collection.First();
        var albumGenres = firstAlbum.Genres;
        Assert.Empty(albumGenres);
    }

    [Fact]
    public void GenrsAreLoadedForEveryAlbum()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums()
            .WithGenres();
        var service = new AlbumService(repo);

        var collection = service.GetAll();

        var firstAlbum = collection.First();
        var albumGenres = firstAlbum.Genres;
        Assert.NotEmpty(albumGenres);
    }

    [Fact]
    public void StylesCollectionIsEmptyIfAlbumHasNoStyles()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums();
        var service = new AlbumService(repo);

        var collection = service.GetAll();

        var firstAlbum = collection.First();
        var albumStyles = firstAlbum.Styles;
        Assert.Empty(albumStyles);
    }

    [Fact]
    public void StylesAreLoadedForEveryAlbum()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums()
            .WithStyles();
        var service = new AlbumService(repo);

        var collection = service.GetAll();

        var firstAlbum = collection.First();
        var albumStyles = firstAlbum.Styles;
        Assert.NotEmpty(albumStyles);
    }
}