using MusicLibrary.Core.ApiClients;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Tests.DiscogsApiClientTests;

public class NoResultsFoundTests
{

    [Fact]
    public void IsTrueWhenDiscogsDtoIsNull()
    {
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByArtistAndTitleParameters(
            artist: "Led Zeppelin",
            title: "Physical Graffiti");

        var discogsResult = client.SearchByArtistAndTitle(apiParameters);

        Assert.True(discogsResult.NoResultsFound);
    }

    [Fact]
    public void IsTrueWhenDiscogsDtoResultsAreNull()
    {
        var httpClient = new HttpClientTest(null!)
            .WithDiscogsDtoWithResultsNull();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByArtistAndTitleParameters(
            artist: "Led Zeppelin",
            title: "Physical Graffiti");

        var discogsResult = client.SearchByArtistAndTitle(apiParameters);

        Assert.True(discogsResult.NoResultsFound);
    }

    [Fact]
    public void IsTrueWhenDiscogsDtoResultsAreEmpty()
    {
        var httpClient = new HttpClientTest(null!)
            .WithDiscogsDtoWithResultsEmpty();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByArtistAndTitleParameters(
            artist: "Led Zeppelin",
            title: "Physical Graffiti");

        var discogsResult = client.SearchByArtistAndTitle(apiParameters);

        Assert.True(discogsResult.NoResultsFound);
    }
}