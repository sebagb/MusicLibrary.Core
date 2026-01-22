using System.Net;
using System.Text.Json;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.ApiClients;

public class DiscogsHttpClient
    (HttpClient httpClient)
{
    private readonly HttpClient httpClient = httpClient;

    public virtual DiscogsResponse GetRequest(string queryParameters)
    {
        var response = httpClient.GetAsync(queryParameters).Result;

        var isSuccessStatusCode = response.IsSuccessStatusCode;
        var tooManyRequests =
            response.StatusCode == HttpStatusCode.TooManyRequests;

        var json = response.Content.ReadAsStringAsync().Result;
        var discogsDto = JsonSerializer.Deserialize<DiscogsDto>(json);

        var discogsResponse = DiscogsResponse.Create(
            discogsDto,
            isSuccessStatusCode,
            tooManyRequests);

        return discogsResponse;
    }
}