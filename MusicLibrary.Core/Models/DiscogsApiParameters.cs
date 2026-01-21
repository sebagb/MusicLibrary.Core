namespace MusicLibrary.Core.Models;

public class DiscogsApiParameters
    (string artist,
    string title,
    string? label = null,
    string? catalogNumber = null)
{
    public readonly string Artist = artist;
    public readonly string Title = title;
    public readonly string? Label = label;
    public readonly string? CatalogNumber = catalogNumber;
}