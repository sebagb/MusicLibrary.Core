using System.Web;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.ApiClients;

public class DiscogsApiClient
    (DiscogsHttpClient client,
    DiscogsAuth auth)
{
    private readonly DiscogsHttpClient client = client;
    private readonly DiscogsAuth auth = auth;

    public DiscogsResultsSummary Search(DiscogsApiParameters apiParameters)
    {
        ArgumentNullException.ThrowIfNull(apiParameters);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiParameters.Artist);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiParameters.Title);

        var queryParameters = GetQueryParameters(apiParameters);

        var response = client.GetResponse(queryParameters);

        if (response.TooManyRequests)
        {
            throw new NotImplementedException();
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new NotImplementedException();
        }

        if (response.DiscogsDto == null)
        {
            return new DiscogsResultsSummary() { NoResultsFound = true };
        }

        return GetDiscogsResultsSummary(response.DiscogsDto.results!);
    }

    private string GetQueryParameters(DiscogsApiParameters apiParameters)
    {
        var parameterCollection = HttpUtility.ParseQueryString(string.Empty);
        parameterCollection.Add("artist", apiParameters.Artist);
        parameterCollection.Add("release_title", apiParameters.Title!);
        parameterCollection.Add("format", "vinyl");
        parameterCollection.Add("key", auth.Key);
        parameterCollection.Add("secret", auth.Secret);

        var parameterString = "?" + parameterCollection.ToString();
        return parameterString;
    }

    private static DiscogsResultsSummary GetDiscogsResultsSummary(IEnumerable<DiscogsDto.ResultDto> dtoCollection)
    {
        return new DiscogsResultsSummary
        {
            Countries = [.. dtoCollection.Select(x => x.country!)],

            CoverImages = [.. dtoCollection.Select(x => x.cover_image)],

            Genres = [.. dtoCollection
                .Where(g => g.genre != null)
                .SelectMany(g => g.genre!)],

            Styles = [.. dtoCollection
                .Where(s => s.style != null)
                .SelectMany(s => s.style!)],

            Years = [.. dtoCollection.Select(x => x.year)]
        };
    }
}