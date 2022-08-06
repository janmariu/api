using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Options;

namespace API.Dota;

public class SteamDetails
{
    public string SteamId { get; set; }
    public string PersonaName { get; set; }
    public string? GameId { get; set; }
}

public class SteamServiceOptions
{
    public string? Key { get; set; }
}

public class SteamService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly string? _key;
    
    public SteamService(IHttpClientFactory httpClientFactory, IOptions<SteamServiceOptions> options)
    {
        if (options.Value.Key == null)
        {
            throw new ArgumentException("Missing STEAM_KEY. Set the environment variable");
        }

        _key = options.Value.Key;
        _httpClient = httpClientFactory.CreateClient();
        _jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
    }
    
    public async Task<List<SteamDetails>?> GetPlayerDetails(params string[] steamIds)
    {
        var ids = string.Join(",", steamIds);

        string url =
            $"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key={_key}&steamids={ids}";

       var result = await _httpClient.GetAsync(url);
       result.EnsureSuccessStatusCode();
       var rawJson = await result.Content.ReadAsStringAsync();
       
       var players = JsonNode.Parse(rawJson)
           ?["response"]
           ?["players"]
           .Deserialize <List<SteamDetails>>(_jsonOptions);

       return players;
    }
}