using MusicLibrary.Core.ApiClients;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Tests.DiscogsApiClientTests;

public class ReleaseIdQueryParametersTests
{
    [Fact]
    public void ReleaseIdIsMappedToQueryParametersString()
    {
        var releaseId = 33342;
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth(string.Empty, string.Empty);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByReleaseIdParameter(
            ReleaseId: releaseId);

        client.SearchByReleaseId(apiParameters);

        var query = httpClient.QueryParameters;
        Assert.Contains($"release_id={releaseId}", query);
    }

    [Fact]
    public void FirstCharacterIsAlwaysInterrogationMark()
    {
        var firstCharacter = '?';
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth(string.Empty, string.Empty);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByReleaseIdParameter(ReleaseId: 0);

        client.SearchByReleaseId(apiParameters);

        var queryFirstCharacter = httpClient.QueryParameters.First();
        Assert.Equal(firstCharacter, queryFirstCharacter);
    }

    [Fact]
    public void AlwaysHasKeyAndSecret()
    {
        var key = "KeyTest";
        var secret = "SecretTest";
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth(key, secret);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsSearchByReleaseIdParameter(ReleaseId: 0);

        client.SearchByReleaseId(apiParameters);

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
        var apiParameters = new DiscogsSearchByReleaseIdParameter(ReleaseId: 0);

        client.SearchByReleaseId(apiParameters);

        var query = httpClient.QueryParameters;
        Assert.Contains($"format={format}", query);
    }
}