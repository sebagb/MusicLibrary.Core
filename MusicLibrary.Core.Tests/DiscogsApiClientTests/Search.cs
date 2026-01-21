using MusicLibrary.Core.ApiClients;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Tests.DiscogsApiClientTests;

public class Search
{
    [Fact]
    public void SearchReturnsDiscogsResultsWithDiscogsDtoValues()
    {
        var httpClient = new HttpClientTest(new HttpClient());
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var album = new Album
        {
            Artist = "Led Zepelin"
        };

        var discogsResult = client.Search(album);

        var country = discogsResult.Countries.First();
        var genre = discogsResult.Genres.First();
        Assert.Equal("Italy", country);
        Assert.Equal("Disco", genre);
    }

    private class HttpClientTest
        (HttpClient httpClient)
        : DiscogsHttpClient(httpClient)
    {
        public override DiscogsDto GetDiscogsDto(string? asd)
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

            return dto;
        }
    }
}