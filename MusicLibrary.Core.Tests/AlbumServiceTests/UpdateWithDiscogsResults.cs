using MusicLibrary.Core.Models;
using MusicLibrary.Core.Services;

namespace MusicLibrary.Core.Tests.AlbumServiceTests;

public class UpdateWithDiscogsResults
{
    [Fact]
    public void ReturnsNullIfAlbumIsNotInTheRepository()
    {
        var repository = new AlbumRepositoryMock()
            .WithAlbum(id: 2222);
        var service = new AlbumService(repository);

        var albumToUpdate = 33242;
        var album = service.UpdateWithDiscogsResults(
            albumToUpdate,
            new DiscogsSelectedResults());

        Assert.Null(album);
    }

    [Fact]
    public void CountriesAreAddedOnlyIfNotInAlbumCountries()
    {
        var albumId = 1111;
        var repository = new AlbumRepositoryMock()
            .WithAlbum(albumId)
            .WithAlbumCountry(
                albumId: 1111,
                countryId: 2020,
                countryName: "Uruguay")
            .WithAlbumCountry(
                albumId: 1111,
                countryId: 2020,
                countryName: "France");
        var service = new AlbumService(repository);
        var results = new DiscogsSelectedResults()
        {
            SelectedCountries = ["Italy", "Uruguay", "Spain"]
        };

        var album = service.UpdateWithDiscogsResults(
            albumId,
            results);

        Assert.Equal(4, album!.Countries.Count);
    }

    [Fact]
    public void AlbumCoverRemainsIfSelectedCoverIsEmpty()
    {
        var albumId = 1111;
        var coverImage = "CoverImageTest";
        var repository = new AlbumRepositoryMock()
            .WithAlbum(albumId)
            .WithAlbumCover(albumId, coverImage);
        var service = new AlbumService(repository);
        var results = new DiscogsSelectedResults()
        {
            SelectedCoverImage = string.Empty
        };

        var album = service.UpdateWithDiscogsResults(
            albumId,
            results);

        Assert.Equal(coverImage, album!.CoverImage);
    }

    [Fact]
    public void AlbumCoverIsReplacedIfSelectedCoverIsNotEmpty()
    {
        var albumId = 1111;
        var coverImage = "CoverImageTest";
        var selectedCoverImage = "SelectedCoverImage";
        var repository = new AlbumRepositoryMock()
            .WithAlbum(albumId)
            .WithAlbumCover(albumId, coverImage);
        var service = new AlbumService(repository);
        var results = new DiscogsSelectedResults()
        {
            SelectedCoverImage = selectedCoverImage
        };

        var album = service.UpdateWithDiscogsResults(
            albumId,
            results);

        Assert.Equal(selectedCoverImage, album!.CoverImage);
    }
}