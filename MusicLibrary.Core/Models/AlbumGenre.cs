namespace MusicLibrary.Core.Models;

public record AlbumGenre
{
    public int Id { get; set; }
    public int AlbumId { get; private set; }
    public Album Album { get; private set; } = new();
    public int GenreId { get; private set; }
    public Genre Genre { get; private set; } = new();

    public AlbumGenre()
    {
        // Needed by Entity Framework
    }

    public AlbumGenre(Album album, Genre genre)
    {
        Album = album;
        AlbumId = album.Id;
        Genre = genre;
        GenreId = genre.Id;
    }
}