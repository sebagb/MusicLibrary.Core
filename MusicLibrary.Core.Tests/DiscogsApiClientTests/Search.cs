using MusicLibrary.Core.ApiClients;
using MusicLibrary.Core.Models;

namespace MusicLibrary.Core.Tests.DiscogsApiClientTests;

public class Search
{
    [Fact]
    public void ThrowsArgumentNullExceptionIfDiscogsApiParametersIsNull()
    {
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);

        Assert.Throws<ArgumentNullException>(
            () => client.Search(null!));
    }

    [Fact]
    public void ThrowsArgumentExceptionIfDiscogsApiParametersArtistIsNullOrWhiteSpace()
    {
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters(
            artist: string.Empty,
            title: "Title");

        Assert.Throws<ArgumentException>(
            () => client.Search(apiParameters));
    }

    [Fact]
    public void ThrowsArgumentExceptionIfDiscogsApiParametersTitleIsNullOrWhiteSpace()
    {
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters(
            artist: "Artist",
            title: string.Empty);

        Assert.Throws<ArgumentException>(
            () => client.Search(apiParameters));
    }

    [Fact]
    public void NoResultsFoundIsTrueWhenDiscogsDtoIsNull()
    {
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters(
            artist: "Led Zeppelin",
            title: "Physical Graffiti");

        var discogsResult = client.Search(apiParameters);

        Assert.True(discogsResult.NoResultsFound);
    }

    [Fact]
    public void NoResultsFoundIsTrueWhenDiscogsDtoResultsAreNull()
    {
        var httpClient = new HttpClientTest(null!)
            .WithDiscogsDtoWithResultsNull();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters(
            artist: "Led Zeppelin",
            title: "Physical Graffiti");

        var discogsResult = client.Search(apiParameters);

        Assert.True(discogsResult.NoResultsFound);
    }

    [Fact]
    public void NoResultsFoundIsTrueWhenDiscogsDtoResultsAreEmpty()
    {
        var httpClient = new HttpClientTest(null!)
            .WithDiscogsDtoWithResultsEmpty();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters(
            artist: "Led Zeppelin",
            title: "Physical Graffiti");

        var discogsResult = client.Search(apiParameters);

        Assert.True(discogsResult.NoResultsFound);
    }

    [Fact]
    public void DuplicateCountriesOnDiscogsDtoResultsAppearOnceOnDiscogsResultsSummary()
    {
        var httpClient = new HttpClientTest(null!).WithDiscogsDto();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters("A", "T");

        var resultsSummary = client.Search(apiParameters);

        var countries = resultsSummary.Countries.Where(x => x.Equals("Italy"));
        Assert.Single(countries);
    }

    [Fact]
    public void DuplicateGeneresOnDiscogsDtoResultsAppearOnceOnDiscogsResultsSummary()
    {
        var httpClient = new HttpClientTest(null!).WithDiscogsDto();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters("A", "T");

        var resultsSummary = client.Search(apiParameters);

        var genres = resultsSummary.Genres.Where(x => x.Equals("Disco"));
        Assert.Single(genres);
    }

    [Fact]
    public void DuplicateStylesOnDiscogsDtoResultsAppearOnceOnDiscogsResultsSummary()
    {
        var httpClient = new HttpClientTest(null!).WithDiscogsDto();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters("A", "T");

        var resultsSummary = client.Search(apiParameters);

        var styles = resultsSummary.Styles.Where(x => x.Equals("Dance"));
        Assert.Single(styles);
    }

    [Fact]
    public void DuplicateYearsOnDiscogsDtoResultsAppearOnceOnDiscogsResultsSummary()
    {
        var httpClient = new HttpClientTest(null!).WithDiscogsDto();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters("A", "T");

        var resultsSummary = client.Search(apiParameters);

        var years = resultsSummary.Years.Where(x => x.Equals("2019"));
        Assert.Single(years);
    }


    [Fact]
    public void DuplicateCoverImagesOnDiscogsDtoResultsAppearOnceOnDiscogsResultsSummary()
    {
        var httpClient = new HttpClientTest(null!).WithDiscogsDto();
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);
        var apiParameters = new DiscogsApiParameters("A", "T");

        var resultsSummary = client.Search(apiParameters);

        var coverImages = resultsSummary.CoverImages.Where(x => x.Equals("url"));
        Assert.Single(coverImages);
    }
}