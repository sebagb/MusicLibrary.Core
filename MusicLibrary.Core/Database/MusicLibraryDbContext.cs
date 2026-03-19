using Microsoft.EntityFrameworkCore;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Database;

public class MusicLibraryDbContext
    (DbContextOptions<MusicLibraryDbContext> options)
    : DbContext(options)
{
    public DbSet<Album> Albums { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<AlbumCountry> AlbumCountries { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<AlbumGenre> AlbumGenres { get; set; }
    public DbSet<Style> Styles { get; set; }
    public DbSet<AlbumStyle> AlbumStyles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>()
            .Ignore(a => a.Countries)
            .Ignore(a => a.Genres)
            .Ignore(a => a.Styles);

        modelBuilder.Entity<AlbumGenre>()
            .HasIndex(a => new { a.AlbumId, a.GenreId })
            .IsUnique();

        modelBuilder.Entity<AlbumCountry>()
            .HasIndex(a => new { a.AlbumId, a.CountryId })
            .IsUnique();

        modelBuilder.Entity<AlbumStyle>()
            .HasIndex(a => new { a.AlbumId, a.StyleId });
    }
}