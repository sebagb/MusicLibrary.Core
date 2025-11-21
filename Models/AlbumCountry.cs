namespace MusicLibrary.Core.Models;

public record AlbumCountry
{
    public int Id { get; set; }
    public int AlbumId { get; private set; }
    public Album Album { get; private set; } = new();
    public int CountryId { get; private set; }
    public Country Country { get; private set; } = new();

    public AlbumCountry()
    {
        // Needed by Entity Framework
    }

    public AlbumCountry(Album album, Country country)
    {
        Album = album;
        AlbumId = album.Id;
        Country = country;
        CountryId = country.Id;
    }
}