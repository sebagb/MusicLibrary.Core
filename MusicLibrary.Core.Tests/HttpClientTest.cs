using MusicLibrary.Core.ApiClients;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Tests;

public class HttpClientTest : DiscogsHttpClient
{
    public string QueryParameters = string.Empty;
    private DiscogsDto? discogsDto = null;
    private bool isSuccessStatusCode;

    private HttpClientTest(HttpClient httpClient) : base(httpClient)
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
        QueryParameters = queryParameters;
        var response = DiscogsResponse.Create(
            discogsDto: discogsDto,
            tooManyRequests: false,
            isSuccessStatusCode: isSuccessStatusCode);

        return response;
    }
}