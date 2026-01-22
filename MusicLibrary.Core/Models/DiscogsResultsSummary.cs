namespace MusicLibrary.Core.Models;

public record DiscogsResultsSummary
{
    public bool NoResultsFound { get; set; }
    public HashSet<string> Countries { get; set; } = [];
    public HashSet<string> CoverImages { get; set; } = [];
    public HashSet<string> Genres { get; set; } = [];
    public HashSet<string> Styles { get; set; } = [];
    public HashSet<string> Years { get; set; } = [];
}