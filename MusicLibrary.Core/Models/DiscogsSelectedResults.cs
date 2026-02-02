namespace MusicLibrary.Core.Models;

public record DiscogsSelectedResults
{
    public HashSet<string> SelectedCountries { get; set; } = [];
    public string? SelectedCoverImage { get; set; }
    public HashSet<string> SelectedFormats { get; set; } = [];
    public HashSet<string> SelectedGenres { get; set; } = [];
    public HashSet<string> SelectedStyles { get; set; } = [];
    public string? SelectedReleaseYear { get; set; }
}