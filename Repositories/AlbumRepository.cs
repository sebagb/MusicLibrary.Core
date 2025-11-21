using Microsoft.EntityFrameworkCore;
using MusicLibrary.Core.Database;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Repositories;

public class AlbumRepository
    (MusicLibraryDbContext context)
    : IAlbumRepository
{
    private readonly MusicLibraryDbContext context = context;

    public void Create(IEnumerable<Album> albumCollection)
    {
        foreach (var album in albumCollection)
        {
            context.Albums.Add(album);
        }
        context.SaveChanges();
    }

    public bool Delete(int id)
    {
        var albumDelete = context.Albums.SingleOrDefault(a => a.Id == id);
        if (albumDelete == null)
        {
            return false;
        }
        context.Albums.Remove(albumDelete);
        return context.SaveChanges() == 1;
    }

    public bool AlbumExistsById(int id)
    {
        return context.Albums.Any(x => x.Id == id);
    }

    public IEnumerable<Album> GetAll()
    {
        var albumCollection = context.Albums;

        var albumCountries = context.AlbumCountries
            .Include(a => a.Country);
        var albumGenres = context.AlbumGenres
            .Include(a => a.Genre);
        var albumStyles = context.AlbumStyles
            .Include(a => a.Style);

        foreach (var album in albumCollection)
        {
            var countries = albumCountries.Where(a =>
                a.AlbumId == album.Id)
                .Select(a => a.Country.Name);

            album.Countries.AddRange(countries);

            var genres = albumGenres.Where(a =>
                a.AlbumId == album.Id)
                .Select(a => a.Genre.Name);

            album.Genres.AddRange(genres);

            var styles = albumStyles.Where(a =>
                a.AlbumId == album.Id)
                .Select(a => a.Style.Name);

            album.Styles.AddRange(styles);
        }

        return albumCollection;
    }

    public Album? GetById(int id)
    {
        var album = context.Albums.SingleOrDefault(a => a.Id == id);

        if (album == null)
        {
            return null;
        }

        var countries = context.AlbumCountries
            .Where(c => c.AlbumId == id)
            .Include(c => c.Country)
            .Select(c => c.Country.Name);

        album.Countries.AddRange(countries);

        var genres = context.AlbumGenres
            .Where(g => g.AlbumId == id)
            .Include(g => g.Genre)
            .Select(g => g.Genre.Name);

        album.Genres.AddRange(genres);

        var styles = context.AlbumStyles
            .Where(s => s.AlbumId == id)
            .Include(s => s.Style)
            .Select(s => s.Style.Name);

        album.Styles.AddRange(styles);

        return album;
    }

    public bool Update(Album album)
    {
        var selectedCountries = album.Countries;
        PersistCountries(album, selectedCountries);

        var selectedCover = album.CoverImage;
        PersistCover(album, selectedCover);

        var selectedGenres = album.Genres;
        PersistGenres(album, selectedGenres);

        var selectedYear = album.ReleaseYear;
        PersistReleaseYear(album, selectedYear);

        var selectedStyles = album.Styles;
        PersistStyles(album, selectedStyles);

        context.Albums.Update(album);
        return context.SaveChanges() > 0;
    }

    private void PersistCountries(Album album, List<string> selectedCountries)
    {
        var existingCountries = context.Countries.Where(c =>
            selectedCountries.Contains(c.Name))
            .ToList();
        foreach (var country in existingCountries)
        {
            var albumCountry = new AlbumCountry(album, country);

            var exists = context.AlbumCountries.Any(c =>
                c.AlbumId == album.Id
                && c.CountryId == country.Id);
            if (!exists)
            {
                context.AlbumCountries.Add(albumCountry);
            }
        }

        var unkownCountries = selectedCountries.Where(selected =>
            !existingCountries.Any(existing =>
                existing.Name.Equals(selected)));
        foreach (var countryName in unkownCountries)
        {
            var newCountry = new Country(countryName);
            var albumCountry = new AlbumCountry(album, newCountry);
            context.Countries.Add(newCountry);
            context.AlbumCountries.Add(albumCountry);
        }
    }

    private void PersistCover(Album album, string? selectedCover)
    {
        var albumHasEmptyCover = string.IsNullOrEmpty(album.CoverImage);
        if (albumHasEmptyCover)
        {
            album.CoverImage = selectedCover;
        }
    }

    private void PersistGenres(Album album, List<string> selectedGenres)
    {
        var existingGenres = context.Genres.Where(g =>
            selectedGenres.Contains(g.Name));
        foreach (var genre in existingGenres)
        {
            var albumGenre = new AlbumGenre(album, genre);

            var exists = context.AlbumGenres.Any(a =>
                a.AlbumId == album.Id
                && a.GenreId == genre.Id);
            if (!exists)
            {
                context.AlbumGenres.Add(albumGenre);
            }
        }

        var unkownGenres = selectedGenres.Where(selected =>
            !existingGenres.Any(existing =>
                existing.Name.Equals(selected)));
        foreach (var genreName in unkownGenres)
        {
            var newGenre = new Genre(genreName);
            var albumGenre = new AlbumGenre(album, newGenre);
            context.Genres.Add(newGenre);
            context.AlbumGenres.Add(albumGenre);
        }
    }

    private void PersistReleaseYear(Album album, string? selectedYear)
    {
        var albumHasEmptyYear = string.IsNullOrEmpty(album.ReleaseYear);
        if (albumHasEmptyYear)
        {
            album.ReleaseYear = selectedYear;
        }
    }

    private void PersistStyles(Album album, List<string> selectedStyles)
    {
        var existingStyles = context.Styles.Where(s =>
            selectedStyles.Contains(s.Name));
        foreach (var style in existingStyles)
        {
            var albumStyle = new AlbumStyles(album, style);

            var exists = context.AlbumStyles.Any(a =>
                a.AlbumId == album.Id
                && a.StyleId == style.Id);
            if (!exists)
            {
                context.AlbumStyles.Add(albumStyle);
            }
        }

        var unkownStyles = selectedStyles.Where(selected =>
            !existingStyles.Any(e =>
                e.Name.Equals(selected)));
        foreach (var styleName in unkownStyles)
        {
            var newStyle = new Style(styleName);
            var albumStyle = new AlbumStyles(album, newStyle);
            context.Styles.Add(newStyle);
            context.AlbumStyles.Add(albumStyle);
        }
    }
}
