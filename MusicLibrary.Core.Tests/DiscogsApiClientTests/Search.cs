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

        Assert.Throws<ArgumentNullException>(
            () => client.Search(null!));
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
        public override DiscogsResponse GetRequest(string queryParameters)
        {
            var r = new DiscogsDto.ResultDto()
            {
                country = "Italy",
                genre = ["Disco"],
            };

            var dto = new DiscogsDto
            {
                results = [r]
            };

            var response = DiscogsResponse.Create(dto);

            return response;
        }
    }
}