using System.Web;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.ApiClients;

public class DiscogsApiClient
    (DiscogsHttpClient client,
    DiscogsAuth auth)
{
    private readonly DiscogsHttpClient client = client;
    private readonly DiscogsAuth auth = auth;

    public DiscogsResultsSummary SearchByArtistAndTitle(
        DiscogsSearchByArtistAndTitleParameters apiParameters)
    {
        ArgumentNullException.ThrowIfNull(apiParameters);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiParameters.Artist);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiParameters.Title);

        var queryParameters = GetArtistAndTitleQueryParameters(apiParameters);

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

    public DiscogsResultsSummary SearchByReleaseId(
        DiscogsSearchByReleaseIdParameter apiParameters)
    {
        ArgumentNullException.ThrowIfNull(apiParameters);

        var queryParameters = GetReleaseIdQueryParameters(apiParameters);

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

    private string GetBaseQueryParameters()
    {
        var parameterCollection = HttpUtility.ParseQueryString(string.Empty);
        parameterCollection.Add("format", "vinyl");
        parameterCollection.Add("key", auth.Key);
        parameterCollection.Add("secret", auth.Secret);

        return parameterCollection.ToString()!;
    }

    private string GetArtistAndTitleQueryParameters(
        DiscogsSearchByArtistAndTitleParameters apiParameters)
    {
        var baseQuery = GetBaseQueryParameters();
        var parameterCollection = HttpUtility.ParseQueryString(baseQuery);
        parameterCollection.Add("artist", apiParameters.Artist);
        parameterCollection.Add("release_title", apiParameters.Title);

        var parameterString = "?" + parameterCollection.ToString();
        return parameterString;
    }

    private string GetReleaseIdQueryParameters(
        DiscogsSearchByReleaseIdParameter apiParameter)
    {
        var baseQuery = GetBaseQueryParameters();
        var parameterCollection = HttpUtility.ParseQueryString(baseQuery);
        parameterCollection.Add("release_id", apiParameter.ReleaseId.ToString());

        var parameterString = "?" + parameterCollection.ToString();
        return parameterString;
    }

    private static DiscogsResultsSummary GetDiscogsResultsSummary(
        IEnumerable<DiscogsDto.ResultDto> dtoCollection)
    {
        return new DiscogsResultsSummary
        {
            Countries = [.. dtoCollection
                .Where(x => x.country != null)
                .Select(x => x.country!)],

            CoverImages = [.. dtoCollection
                .Where(x => x.cover_image != null)
                .Select(x => x.cover_image!)],

            Genres = [.. dtoCollection
                .Where(g => g.genre != null)
                .SelectMany(g => g.genre)],

            Styles = [.. dtoCollection
                .Where(s => s.style != null)
                .SelectMany(s => s.style)],

            Years = [.. dtoCollection
                .Where(x => x.year != null)
                .Select(x => x.year!)]
        };
    }
}