namespace MusicLibrary.Core.ApiClients;

public class DiscogsAuth
    (string key,
    string secret)
{
    public readonly string Key = key;
    public readonly string Secret = secret;
}