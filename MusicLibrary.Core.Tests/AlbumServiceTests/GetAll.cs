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
    public void CountryNamesAreLoadedForEveryAlbum()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums()
            .WithCountries();
        var service = new AlbumService(repo);

        var collection = service.GetAll();

        var firstAlbum = collection.First();
        var albumCountries = firstAlbum.Countries;
        Assert.Equal(2, albumCountries.Count);
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
    public void GenreNamesAreLoadedForEveryAlbum()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums()
            .WithGenres();
        var service = new AlbumService(repo);

        var collection = service.GetAll();

        var firstAlbum = collection.First();
        var albumGenres = firstAlbum.Genres;
        Assert.Single(albumGenres);
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
    public void StyleNamesAreLoadedForEveryAlbum()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbums()
            .WithStyles();
        var service = new AlbumService(repo);

        var collection = service.GetAll();

        var firstAlbum = collection.First();
        var albumStyles = firstAlbum.Styles;
        Assert.Equal(3, albumStyles.Count);
    }
}