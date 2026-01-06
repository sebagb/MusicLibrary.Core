using MusicLibrary.Core.Models;
using MusicLibrary.Core.Repositories;

namespace MusicLibrary.Core.Tests;

public class AlbumRepositoryMock : IAlbumRepository
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
        return albums.SingleOrDefault(x => x.Id == id);
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


    public AlbumRepositoryMock WithAlbum(
        int id = 1111,
        string? artist = "AlbumArtist",
        string? title = "AlbumTitle",
        string? label = "AlbumLabel",
        string? catalogNumber = "AlbumCatalogNumber",
        decimal priceEuro = 10,
        DateOnly aquiredDate = new DateOnly())
    {
        var album = new Album
        {
            Id = id,
            Artist = artist,
            Title = title,
            Label = label,
            CatalogNumber = catalogNumber,
            PriceEuro = priceEuro,
            AquiredDate = aquiredDate
        };
        albums.Add(album);

        return this;
    }

    public AlbumRepositoryMock WithCountries()
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

    public AlbumRepositoryMock WithGenres()
    {
        genres = [
            new Genre("Rock"),
            new Genre("Pop"),
            new Genre("Grunge")];

        albumGenres = [
            new AlbumGenre(albums.First(), genres.First())];

        return this;
    }

    public AlbumRepositoryMock WithStyles()
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