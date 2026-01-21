using System.Net;
using System.Text.Json;
using System.Web;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.ApiClients;

public class DiscogsApiClient
    (HttpClient httpClient,
    DiscogsAuth auth)
{
    private readonly HttpClient httpClient = httpClient;
    private readonly DiscogsAuth auth = auth;

    public DiscogsResults Search(Album album)
    {
        if (string.IsNullOrEmpty(album.Artist)
            && string.IsNullOrEmpty(album.Title)
            && string.IsNullOrEmpty(album.Label)
            && string.IsNullOrEmpty(album.CatalogNumber))
        {
            throw new NotImplementedException();
        }

        var queryParameters = GetAlbumParameters(album);

        var discogsDto = GetDiscogsDto(queryParameters);

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

    private DiscogsDto GetDiscogsDto(string queryParameters)
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

    private string GetAlbumParameters(Album album)
    {
        var parameterCollection = HttpUtility.ParseQueryString(string.Empty);

        var includeArtist = !string.IsNullOrEmpty(album.Artist);
        if (includeArtist)
        {
            parameterCollection.Add("artist", album.Artist);
        }

        var includeTitle = !string.IsNullOrEmpty(album.Title);
        if (includeTitle)
        {
            parameterCollection.Add("release_title", album.Title!);
        }

        var includeLabel = !string.IsNullOrEmpty(album.Label);
        if (includeLabel)
        {
            parameterCollection.Add("label", album.Label!);
        }

        var includeCatalogNumber = !string.IsNullOrEmpty(album.CatalogNumber);
        if (includeCatalogNumber)
        {
            parameterCollection.Add("catno", album.CatalogNumber!);
        }

        parameterCollection.Add("format", "vinyl");
        parameterCollection.Add("key", auth.Key);
        parameterCollection.Add("secret", auth.Secret);

        var parameterString = "?" + parameterCollection.ToString();
        return parameterString!;
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