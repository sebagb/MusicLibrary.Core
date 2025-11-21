namespace MusicLibrary.Core.Models;

public record Country
{
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;

    public Country()
    {
        // Needed by Entity Framework
    }

    public Country(string name)
    {
        Name = name;
    }
}