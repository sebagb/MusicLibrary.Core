using MusicLibrary.Core.ApiClients;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Tests.DiscogsApiClientTests;

public class Search
{
    [Fact]
    public void ThrowsArgumentNullExceptionIfDiscogsApiParametersIsNull()
    {
        var httpClient = new HttpClientTest(new HttpClient());
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);

        Assert.Throws<ArgumentNullException>(() => client.Search(null));
    }

    [Fact]
    public void ThrowsArgumentExceptionIfDiscogsApiParametersArtistIsNullOrWhiteSpace()
    {
        var httpClient = new HttpClientTest(new HttpClient());
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
        var httpClient = new HttpClientTest(new HttpClient());
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters(
            artist: "Artist",
            title: string.Empty);

        Assert.Throws<ArgumentException>(
            () => client.Search(apiParameters));
    }

    [Fact]
    public void QueryParametersFirstCharacterIsAlwaysInterrogationMark()
    {
        var firstCharacter = '?';
        var httpClient = new HttpClientTest(new HttpClient());
        var auth = new DiscogsAuth(string.Empty, string.Empty);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters("A", "T");

        client.Search(apiParameters);

        var queryFirstCharacter = httpClient.QueryParameters.First();
        Assert.Equal(firstCharacter, queryFirstCharacter);
    }

    [Fact]
    public void QueryParametersArtistIsEqualToDiscogsApiParametersArtist()
    {
        var artist = "LedZeppo";
        var httpClient = new HttpClientTest(new HttpClient());
        var auth = new DiscogsAuth(string.Empty, string.Empty);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters(
            artist: artist,
            title: "T");

        client.Search(apiParameters);

        var query = httpClient.QueryParameters;
        Assert.Contains($"artist={artist}", query);
    }

    [Fact]
    public void QueryParametersTitleIsEqualToDiscogsApiParametersTitle()
    {
        var title = "Meddle";
        var httpClient = new HttpClientTest(new HttpClient());
        var auth = new DiscogsAuth(string.Empty, string.Empty);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters(
            artist: "A",
            title: title);

        client.Search(apiParameters);

        var query = httpClient.QueryParameters;
        Assert.Contains($"release_title={title}", query);
    }

    [Fact]
    public void QueryParametersAlwaysHasKeyAndSecret()
    {
        var key = "KeyTest";
        var secret = "SecretTest";
        var httpClient = new HttpClientTest(new HttpClient());
        var auth = new DiscogsAuth(key, secret);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters("A", "T");

        client.Search(apiParameters);

        var query = httpClient.QueryParameters;
        Assert.Contains($"key={key}", query);
        Assert.Contains($"secret={secret}", query);
    }

    [Fact]
    public void QueryParametersAlwaysHasFormatEqualToVinyl()
    {
        var format = "vinyl";
        var httpClient = new HttpClientTest(new HttpClient());
        var auth = new DiscogsAuth(string.Empty, string.Empty);
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters("A", "T");

        client.Search(apiParameters);

        var query = httpClient.QueryParameters;
        Assert.Contains($"format={format}", query);
    }

    [Fact]
    public void SearchReturnsDiscogsResultsWithDiscogsDtoValues()
    {
        var httpClient = new HttpClientTest(new HttpClient());
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

    private class HttpClientTest
        (HttpClient httpClient)
        : DiscogsHttpClient(httpClient)
    {
        public string QueryParameters = string.Empty;

        public override DiscogsDto GetDiscogsDto(string queryParameters)
        {
            QueryParameters = queryParameters;

            var r = new DiscogsDto.ResultDto()
            {
                country = "Italy",
                genre = ["Disco"],
            };

            var dto = new DiscogsDto
            {
                results = [r]
            };

            return dto;
        }
    }
}