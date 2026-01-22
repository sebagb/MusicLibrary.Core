namespace MusicLibrary.Core.Models;

public record DiscogsDto
{
    public PaginationDto? pagination { get; set; }
    public IEnumerable<ResultDto> results { get; set; } = [];

    public sealed record PaginationDto
    {
        public int page { get; set; }
        public int pages { get; set; }
        public int per_page { get; set; }
        public int items { get; set; }
    }

    public record ResultDto
    {
        public int id { get; set; }
        public string? country { get; set; }
        public string? year { get; set; }
        public IEnumerable<string> label { get; set; } = [];
        public IEnumerable<string> genre { get; set; } = [];
        public IEnumerable<string> style { get; set; } = [];
        public string? cover_image { get; set; }
    }
}