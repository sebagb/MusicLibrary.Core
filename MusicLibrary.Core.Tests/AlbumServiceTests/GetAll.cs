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

    [Fact]
    public void DefaultOrderIsByAscendingArtistAndThenByTitle()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(id: 8888, artist: "Yang", title: "Blues")
            .WithAlbum(id: 4444, artist: "Yang", title: "Rock")
            .WithAlbum(id: 1111, artist: "Yang", title: "Zeep")
            .WithAlbum(id: 4343, artist: "Bang", title: "Bong")
            .WithAlbum(id: 2222, artist: "Yang", title: "Jazz");
        var service = new AlbumService(repo);
        var options = new GetAllAlbumOptions()
        {
            SortOrder = GetAllAlbumSortOrder.Unsorted
        };

        var collection = service.GetAll(options);

        Assert.Equal(4343, collection.ElementAt(0).Id);
        Assert.Equal(8888, collection.ElementAt(1).Id);
        Assert.Equal(2222, collection.ElementAt(2).Id);
        Assert.Equal(4444, collection.ElementAt(3).Id);
        Assert.Equal(1111, collection.ElementAt(4).Id);
    }

    [Fact]
    public void SortByArtistWithSameNameIsThenSortedByTitle()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(id: 8888, artist: "Yang", title: "Blues")
            .WithAlbum(id: 4444, artist: "Yang", title: "Rock")
            .WithAlbum(id: 1111, artist: "Yang", title: "Zeep")
            .WithAlbum(id: 4343, artist: "Bang", title: "Bong")
            .WithAlbum(id: 2222, artist: "Yang", title: "Jazz");
        var service = new AlbumService(repo);
        var options = new GetAllAlbumOptions()
        {
            SortField = GetAllAlbumSortField.Artist,
            SortOrder = GetAllAlbumSortOrder.Descending
        };

        var collection = service.GetAll(options);

        Assert.Equal(1111, collection.ElementAt(0).Id);
        Assert.Equal(4444, collection.ElementAt(1).Id);
        Assert.Equal(2222, collection.ElementAt(2).Id);
        Assert.Equal(8888, collection.ElementAt(3).Id);
        Assert.Equal(4343, collection.ElementAt(4).Id);
    }

    [Fact]
    public void SortByTitleWithSameNameIsThenSortedByArtist()
    {
        var repo = new AlbumRepositoryMock()
            .WithAlbum(id: 8888, artist: "The Beatles", title: "Please Please Me")
            .WithAlbum(id: 2222, artist: "The Replacements", title: "Let It Be")
            .WithAlbum(id: 1111, artist: "The Beatles", title: "Let It Be")
            .WithAlbum(id: 4343, artist: "Abba", title: "Voulez-Vous")
            .WithAlbum(id: 4444, artist: "The Rollings", title: "Tatto You");
        var service = new AlbumService(repo);
        var options = new GetAllAlbumOptions()
        {
            SortField = GetAllAlbumSortField.Title,
            SortOrder = GetAllAlbumSortOrder.Ascending
        };

        var collection = service.GetAll(options);

        Assert.Equal(1111, collection.ElementAt(0).Id);
        Assert.Equal(2222, collection.ElementAt(1).Id);
        Assert.Equal(8888, collection.ElementAt(2).Id);
        Assert.Equal(4444, collection.ElementAt(3).Id);
        Assert.Equal(4343, collection.ElementAt(4).Id);
    }
}