using System.Web;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.ApiClients;

public class DiscogsApiClient
    (DiscogsHttpClient client,
    DiscogsAuth auth)
{
    private readonly DiscogsHttpClient client = client;
    private readonly DiscogsAuth auth = auth;

    public DiscogsResults Search(DiscogsApiParameters apiParameters)
    {
        ArgumentNullException.ThrowIfNull(apiParameters);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiParameters.Artist);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiParameters.Title);

        var queryParameters = GetQueryParameters(apiParameters);

        var discogsDto = client.GetDiscogsDto(queryParameters);

        if (discogsDto == null || discogsDto.results == null)
        {
            throw new NotImplementedException();
        }

        var hasNoResults = discogsDto == null || !discogsDto.results.Any();
        if (hasNoResults)
        {
            throw new NotImplementedException();
        }

        var groupedResults = GroupResultSets(discogsDto!.results);
        return groupedResults;
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

    private static DiscogsResults GroupResultSets(IEnumerable<DiscogsDto.ResultDto> dtoCollection)
    {
        return new DiscogsResults
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