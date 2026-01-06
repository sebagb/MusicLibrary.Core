namespace MusicLibrary.Core.Models;

public class GetAllAlbumOptions
{
    public SortFields SortField { get; set; }
    public SortOrders SortOrder { get; set; }
    public string ArtistFilter { get; set; } = string.Empty;
    public string TitleFilter { get; set; } = string.Empty;
    public string LabelFilter { get; set; } = string.Empty;
    public string CountryFilter { get; set; } = string.Empty;
    public string GenresFilter { get; set; } = string.Empty;
    public string StylesFilter { get; set; } = string.Empty;
    public enum SortFields
    {
        Artist,
        Title,
        Label,
        Country,
        Genres,
        Styles,
    }
    public enum SortOrders { Unsorted, Ascending, Descending }
}








