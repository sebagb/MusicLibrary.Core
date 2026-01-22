using MusicLibrary.Core.ApiClients;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Tests.DiscogsApiClientTests;

public class Search
{
    [Fact]
    public void ThrowsArgumentNullExceptionIfDiscogsApiParametersIsNull()
    {
        var httpClient = HttpClientTest.Create();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);

        Assert.Throws<ArgumentNullException>(
            () => client.Search(null!));
    }

    [Fact]
    public void ThrowsArgumentExceptionIfDiscogsApiParametersArtistIsNullOrWhiteSpace()
    {
        var httpClient = HttpClientTest.Create();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters(
            artist: string.Empty,
            title: "Title");

        Assert.Throws<ArgumentException>(
            () => client.Search(apiParameters));
    }

    [Fact]
    public void ThrowsArgumentExceptionIfDiscogsApiParametersTitleIsNullOrWhiteSpace()
    {
        var httpClient = HttpClientTest.Create();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters(
            artist: "Artist",
            title: string.Empty);

        Assert.Throws<ArgumentException>(
            () => client.Search(apiParameters));
    }

    [Fact]
    public void SearchReturnsDiscogsResultsWithDiscogsDtoValues()
    {
        var httpClient = HttpClientTest.Create();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters(
            artist: "Led Zeppelin",
            title: "Physical Graffiti");

        var discogsResult = client.Search(apiParameters);

        var country = discogsResult.Countries.First();
        var genre = discogsResult.Genres.First();
        Assert.Equal("Italy", country);
        Assert.Equal("Disco", genre);
    }
}