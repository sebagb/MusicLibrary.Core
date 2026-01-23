using MusicLibrary.Core.ApiClients;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Tests.DiscogsApiClientTests;

public class DiscogsSearchByArtistAndTitleTests
{
    [Fact]
    public void ThrowsArgumentNullExceptionIfDiscogsApiParametersIsNull()
    {
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);

        Assert.Throws<ArgumentNullException>(
            () => client.SearchByArtistAndTitle(null!));
    }

    [Fact]
    public void ThrowsArgumentExceptionIfDiscogsApiParametersArtistIsNullOrWhiteSpace()
    {
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByArtistAndTitleParameters(
            artist: string.Empty,
            title: "Title");

        Assert.Throws<ArgumentException>(
            () => client.SearchByArtistAndTitle(apiParameters));
    }

    [Fact]
    public void ThrowsArgumentExceptionIfDiscogsApiParametersTitleIsNullOrWhiteSpace()
    {
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByArtistAndTitleParameters(
            artist: "Artist",
            title: string.Empty);

        Assert.Throws<ArgumentException>(
            () => client.SearchByArtistAndTitle(apiParameters));
    }
}