namespace MusicLibrary.Core.Models;

public record Style
{
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;

    public Style()
    {
        // Needed by Entity Framework
    }

    public Style(string name)
    {
        Name = name;
    }
}