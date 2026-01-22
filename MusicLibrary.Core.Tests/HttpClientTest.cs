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

    public HttpClientTest WithResults()
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