using MusicLibrary.Core.Models;
using MusicLibrary.Core.Services;

namespace MusicLibrary.Core.Tests.AlbumServiceTests;

public class GetAll
{
    [Fact]
    public void CountriesCollectionIsEmptyIfAlbumHasNoCountries()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222);
        var service = new AlbumService(repo);

        var collection = service.GetAll(new GetAllAlbumOptions());

        var firstAlbum = collection.First();
        var albumCountries = firstAlbum.Countries;
        Assert.Empty(albumCountries);
    }

    [Fact]
    public void CountriesAreLoadedForEveryAlbum()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222)
            .WithCountries();
        var service = new AlbumService(repo);

        var collection = service.GetAll(new GetAllAlbumOptions());

        var firstAlbum = collection.First();
        var albumCountries = firstAlbum.Countries;
        Assert.NotEmpty(albumCountries);
    }

    [Fact]
    public void GenresCollectionIsEmptyIfAlbumHasNoGenres()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222);
        var service = new AlbumService(repo);

        var collection = service.GetAll(new GetAllAlbumOptions());

        var firstAlbum = collection.First();
        var albumGenres = firstAlbum.Genres;
        Assert.Empty(albumGenres);
    }

    [Fact]
    public void GenrsAreLoadedForEveryAlbum()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222)
            .WithGenres();
        var service = new AlbumService(repo);

        var collection = service.GetAll(new GetAllAlbumOptions());

        var firstAlbum = collection.First();
        var albumGenres = firstAlbum.Genres;
        Assert.NotEmpty(albumGenres);
    }

    [Fact]
    public void StylesCollectionIsEmptyIfAlbumHasNoStyles()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222);
        var service = new AlbumService(repo);

        var collection = service.GetAll(new GetAllAlbumOptions());

        var firstAlbum = collection.First();
        var albumStyles = firstAlbum.Styles;
        Assert.Empty(albumStyles);
    }

    [Fact]
    public void StylesAreLoadedForEveryAlbum()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(1111)
            .WithAlbum(2222)
            .WithStyles();
        var service = new AlbumService(repo);

        var collection = service.GetAll(new GetAllAlbumOptions());

        var firstAlbum = collection.First();
        var albumStyles = firstAlbum.Styles;
        Assert.NotEmpty(albumStyles);
    }

    [Fact]
    public void AlbumIsFilteredByTitle()
    {
        var searchedTitle = "Believer";
        var repo = new AlbumRepositoryMock()
            .WithAlbum(title: "Submarino")
            .WithAlbum(title: "Eclipse")
            .WithAlbum(title: searchedTitle)
            .WithAlbum(title: "Nocturnalia");
        var service = new AlbumService(repo);
        var options = new GetAllAlbumOptions()
        {
            TitleFilter = searchedTitle
        };

        var collection = service.GetAll(options);

        var firstAlbum = collection.First();
        Assert.Equal(searchedTitle, firstAlbum.Title);
    }

    [Fact]
    public void TitleFilterIsNotAppliedIfEmpty()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(title: "Submarino")
            .WithAlbum(title: "Eclipse")
            .WithAlbum(title: "Nocturnalia");
        var service = new AlbumService(repo);
        var options = new GetAllAlbumOptions()
        {
            TitleFilter = string.Empty
        };

        var collection = service.GetAll(options);

        Assert.Equal(3, collection.Count());
    }

    [Fact]
    public void AlbumIsFilteredByArtist()
    {
        var searchedArtist = "Jimi";
        var repo = new AlbumRepositoryMock()
            .WithAlbum(artist: "Joe")
            .WithAlbum(artist: searchedArtist)
            .WithAlbum(artist: "Dan");
        var service = new AlbumService(repo);
        var options = new GetAllAlbumOptions()
        {
            ArtistFilter = searchedArtist
        };

        var collection = service.GetAll(options);

        var album = collection.First();
        Assert.Equal(searchedArtist, album.Artist);
    }

    [Fact]
    public void AlbumFilterIsNotAppliedIfEmpty()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(artist: "Joe")
            .WithAlbum(artist: "Dan");
        var service = new AlbumService(repo);
        var options = new GetAllAlbumOptions()
        {
            ArtistFilter = string.Empty
        };

        var collection = service.GetAll(options);

        Assert.Equal(2, collection.Count());
    }
}