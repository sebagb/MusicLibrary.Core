namespace MusicLibrary.Core.Models;

public record Genre
{
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;

    public Genre()
    {
        // Needed by Entity Framework
    }

    public Genre(string name)
    {
        Name = name;
    }
}