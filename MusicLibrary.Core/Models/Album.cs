namespace MusicLibrary.Core.Models;

public class Album
{
    public int Id { get; set; }
    public string? Artist { get; set; }
    public string? Title { get; set; }
    public string? Label { get; set; }
    public string? CatalogNumber { get; set; }
    public decimal PriceEuro { get; set; }
    public DateOnly AquiredDate { get; set; }
    public string? Seller { get; set; }
    public List<string> Countries { get; set; } = [];
    public string? CoverImage { get; set; }
    public List<string> Genres { get; set; } = [];
    public string? ReleaseYear { get; set; }
    public List<string> Styles { get; set; } = [];
}