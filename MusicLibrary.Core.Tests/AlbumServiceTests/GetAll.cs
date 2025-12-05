using MusicLibrary.Core.Models;
using MusicLibrary.Core.Repositories;
using MusicLibrary.Core.Services;

namespace MusicLibrary.Core.Tests.AlbumServiceTests;

public class AlbumServiceTests
{
    [Fact]
    public void CountriesNamesAreLoadedToAlbumCountriesWhenRetrievingAlbums()
    {
        var repo = new AlbumRepositoryTest()
            .WithAlbums()
            .WithCountries();
        var service = new AlbumService(repo);

        var collection = service.GetAll();

        var firstAlbum = collection.First();
        var albumCountries = firstAlbum.Countries;
        Assert.Equal(2, albumCountries.Count);
    }

    [Fact]
    public void GenresNamesAreLoadedToAlbumGenresWhenRetrievingAlbums()
    {
        var repo = new AlbumRepositoryTest()
            .WithAlbums()
            .WithGenres();
        var service = new AlbumService(repo);

        var collection = service.GetAll();

        var firstAlbum = collection.First();
        var albumGenres = firstAlbum.Genres;
        Assert.Single(albumGenres);
    }

    [Fact]
    public void StylesNamesAreLoadedToAlbumStylesWhenRetrievingAlbums()
    {
        var repo = new AlbumRepositoryTest()
            .WithAlbums()
            .WithStyles();
        var service = new AlbumService(repo);

        var collection = service.GetAll();

        var firstAlbum = collection.First();
        var albumStyles = firstAlbum.Styles;
        Assert.Equal(3, albumStyles.Count);
    }
}

public class AlbumRepositoryTest : IAlbumRepository
{
    private ICollection<Album> albums = [];
    private ICollection<Country> countries = [];
    private ICollection<AlbumCountry> albumCountries = [];
    private ICollection<Genre> genres = [];
    private ICollection<AlbumGenre> albumGenres = [];
    private ICollection<Style> styles = [];
    private ICollection<AlbumStyles> albumStyles = [];

    public bool AlbumExistsById(int id)
    {
        throw new NotImplementedException();
    }

    public void Create(IEnumerable<Album> album)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Album> GetAll()
    {
        return albums;
    }

    public Album? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public bool Update(Album album)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> GetAlbumCountries(int albumId)
    {
        return albumCountries
            .Where(x => x.AlbumId == albumId)
            .Select(x => x.Country.Name);
    }

    public IEnumerable<string> GetAlbumGenres(int albumId)
    {
        return albumGenres
            .Where(x => x.AlbumId == albumId)
            .Select(x => x.Genre.Name);
    }

    public IEnumerable<string> GetAlbumStyles(int albumId)
    {
        return albumStyles
            .Where(x => x.AlbumId == albumId)
            .Select(x => x.Style.Name);
    }


    public AlbumRepositoryTest WithAlbums()
    {
        albums = [
            new Album() { Id = 1111 },
            new Album() { Id = 2222 },
            new Album() { Id = 3333 }];

        return this;
    }

    public AlbumRepositoryTest WithCountries()
    {
        countries = [
            new Country("Italy") { Id = 1111 },
            new Country("Spain") { Id = 2222 },
            new Country("France") { Id = 3333 }];

        albumCountries = [
            new AlbumCountry(albums.ElementAt(0), countries.ElementAt(0)),
            new AlbumCountry(albums.ElementAt(0), countries.ElementAt(2))];

        return this;
    }

    public AlbumRepositoryTest WithGenres()
    {
        genres = [
            new Genre("Rock"),
            new Genre("Pop"),
            new Genre("Grunge")];

        albumGenres = [
            new AlbumGenre(albums.First(), genres.First())];

        return this;
    }

    public AlbumRepositoryTest WithStyles()
    {
        styles = [
            new Style("Chill"),
            new Style("Road"),
            new Style("Beach")];

        albumStyles = [
            new AlbumStyles(albums.First(), styles.ElementAt(0)),
            new AlbumStyles(albums.First(), styles.ElementAt(1)),
            new AlbumStyles(albums.First(), styles.ElementAt(2))];

        return this;
    }
}