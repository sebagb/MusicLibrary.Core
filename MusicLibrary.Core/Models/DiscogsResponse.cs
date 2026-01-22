namespace MusicLibrary.Core.Models;

public class DiscogsResponse
{
    public DiscogsDto? DiscogsDto { get; private set; }
    public bool TooManyRequests { get; private set; }
    public bool IsSuccessStatusCode { get; private set; }

    private DiscogsResponse()
    {

    }

    public static DiscogsResponse Create
        (DiscogsDto? discogsDto,
        bool tooManyRequests = false,
        bool isSuccessStatusCode = false)
    {
        var response = new DiscogsResponse();

        if (discogsDto != null
            && discogsDto.results != null
            && discogsDto.results.Any())
        {
            response.DiscogsDto = discogsDto;
        }
        response.TooManyRequests = tooManyRequests;
        response.IsSuccessStatusCode = isSuccessStatusCode;

        return response;
    }
}