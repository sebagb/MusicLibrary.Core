
using MusicLibrary.Core.ApiClients;

namespace MusicLibrary.Core.Tests.DiscogsApiClientTests;

public class DiscogsSearchByReleaseIdTests
{
    [Fact]
    public void ThrowsArgumentNullExceptionIfDiscogsApiParametersIsNull()
    {
        var httpClient = new HttpClientTest(null!);
        var auth = new DiscogsAuth("KeyTest", "SecretTest");
        var client = new DiscogsApiClient(httpClient, auth);

        Assert.Throws<ArgumentNullException>(
            () => client.SearchByReleaseId(null!));
    }
}