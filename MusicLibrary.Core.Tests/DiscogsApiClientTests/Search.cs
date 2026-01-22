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

    private class HttpClientTest : DiscogsHttpClient
    {
        private DiscogsDto? discogsDto = null;
        private bool isSuccessStatusCode;

        private HttpClientTest(HttpClient h) : base(h)
        {

        }

        public static HttpClientTest Create()
        {
            var httpClient = new HttpClientTest(null!)
            {
                isSuccessStatusCode = true,
                discogsDto = new DiscogsDto
                {
                    results = [new DiscogsDto.ResultDto()
                    {
                        country = "Italy",
                        genre = ["Disco"],
                    }]
                }
            };
            return httpClient;

        }

        public void WithResults()
        {
            var results = new DiscogsDto.ResultDto()
            {
                country = "Italy",
                genre = ["Disco"],
            };

            discogsDto = new DiscogsDto
            {
                results = [results]
            };
        }

        public void WithSuccessStatusCode()
        {
            isSuccessStatusCode = true;
        }

        public override DiscogsResponse GetResponse(string queryParameters)
        {
            var response = DiscogsResponse.Create(
                discogsDto: discogsDto,
                tooManyRequests: false,
                isSuccessStatusCode: isSuccessStatusCode);

            return response;
        }
    }
}