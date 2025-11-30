namespace MusicLibrary.Core.Models;

public record AlbumStyles
{
    public int Id { get; set; }
    public int AlbumId { get; private set; }
    public Album Album { get; private set; } = new();
    public int StyleId { get; private set; }
    public Style Style { get; private set; } = new();

    public AlbumStyles()
    {
        // Needed by Entity Framework
    }

    public AlbumStyles(Album album, Style style)
    {
        Album = album;
        AlbumId = album.Id;
        Style = style;
        StyleId = style.Id;
    }
}