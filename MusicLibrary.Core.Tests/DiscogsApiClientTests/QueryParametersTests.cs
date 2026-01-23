using MusicLibrary.Core.ApiClients;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Tests.DiscogsApiClientTests;

public class QueryParameters
{
    [Fact]
    public void FirstCharacterIsAlwaysInterrogationMark()
    {
        var firstCharacter = '?';
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth(string.Empty, string.Empty);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByArtistAndTitleParameters(
            artist: "A",
            title: "T");

        client.SearchByArtistAndTitle(apiParameters);

        var queryFirstCharacter = httpClient.QueryParameters.First();
        Assert.Equal(firstCharacter, queryFirstCharacter);
    }

    [Fact]
    public void ArtistIsEqualToDiscogsApiParametersArtist()
    {
        var artist = "LedZeppo";
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth(string.Empty, string.Empty);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByArtistAndTitleParameters(
            artist: artist,
            title: "T");

        client.SearchByArtistAndTitle(apiParameters);

        var query = httpClient.QueryParameters;
        Assert.Contains($"artist={artist}", query);
    }

    [Fact]
    public void TitleIsEqualToDiscogsApiParametersTitle()
    {
        var title = "Meddle";
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth(string.Empty, string.Empty);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByArtistAndTitleParameters(
            artist: "A",
            title: title);

        client.SearchByArtistAndTitle(apiParameters);

        var query = httpClient.QueryParameters;
        Assert.Contains($"release_title={title}", query);
    }

    [Fact]
    public void AlwaysHasKeyAndSecret()
    {
        var key = "KeyTest";
        var secret = "SecretTest";
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth(key, secret);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByArtistAndTitleParameters(
            artist: "A",
            title: "T");

        client.SearchByArtistAndTitle(apiParameters);

        var query = httpClient.QueryParameters;
        Assert.Contains($"key={key}", query);
        Assert.Contains($"secret={secret}", query);
    }

    [Fact]
    public void AlwaysHasFormatEqualToVinyl()
    {
        var format = "vinyl";
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth(string.Empty, string.Empty);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByArtistAndTitleParameters(
            artist: "A",
            title: "T");

        client.SearchByArtistAndTitle(apiParameters);

        var query = httpClient.QueryParameters;
        Assert.Contains($"format={format}", query);
    }
}