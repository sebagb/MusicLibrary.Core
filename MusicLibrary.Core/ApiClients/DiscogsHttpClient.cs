using System.Net;
using System.Text.Json;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.ApiClients;

public class DiscogsHttpClient
    (HttpClient httpClient)
{
    private readonly HttpClient httpClient = httpClient;

    public virtual DiscogsDto GetDiscogsDto(string queryParameters)
    {
        var response = httpClient.GetAsync(queryParameters).Result;

        if (!response.IsSuccessStatusCode)
        {
            var exceededRequests =
                response.StatusCode == HttpStatusCode.TooManyRequests;
            if (exceededRequests)
            {
                throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }

        var json = response.Content.ReadAsStringAsync().Result;

        return JsonSerializer.Deserialize<DiscogsDto>(json);
    }
}