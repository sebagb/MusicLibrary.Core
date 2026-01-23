using MusicLibrary.Core.ApiClients;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Tests;

public class HttpClientTest
    (HttpClient httpClient)
    : DiscogsHttpClient(httpClient)
{
    public string QueryParameters = string.Empty;
    private DiscogsDto? discogsDto = null;
    private bool isSuccessStatusCode = true;

    public HttpClientTest WithDiscogsDto()
    {
        discogsDto = new DiscogsDto
        {
            results = [
                new DiscogsDto.ResultDto()
                {
                    country = "Italy",
                    genre = ["Disco", "Pop"],
                    style = ["Dance"],
                },
                new DiscogsDto.ResultDto() {
                    id = 1111,
                    country = "Uruguay",
                    year = "2019",
                    genre = ["Disco", "Candombe"],
                    style = ["Dance"],
                    cover_image = "url"
                },
                new DiscogsDto.ResultDto() {
                    id = 2222,
                    country = "Italy",
                    year = "2019",
                    genre = ["Pop", "Candombe"],
                    style = ["Dance"],
                    cover_image = "url"
                },
                new DiscogsDto.ResultDto() {
                    id = 3333,
                    year = "2019",
                    genre = ["Pop", "Candombe"],
                    style = ["Dance"],
                }]
        };

        return this;
    }

    public HttpClientTest WithDiscogsDtoWithResultsNull()
    {
        discogsDto = new DiscogsDto();
        return this;
    }

    public HttpClientTest WithDiscogsDtoWithResultsEmpty()
    {
        discogsDto = new DiscogsDto()
        {
            results = []
        };
        return this;
    }

    public HttpClientTest WithSuccessStatusCode()
    {
        isSuccessStatusCode = true;
        return this;
    }

    public override DiscogsResponse GetResponse(string queryParameters)
    {
        QueryParameters = queryParameters;
        var response = DiscogsResponse.Create(
            discogsDto: discogsDto,
            tooManyRequests: false,
            isSuccessStatusCode: isSuccessStatusCode);

        return response;
    }
}