namespace MusicLibrary.Core.Models;

public class DiscogsSearchByArtistAndTitleParameters
    (string artist,
    string title)
{
    public readonly string Artist = artist;
    public readonly string Title = title;
}