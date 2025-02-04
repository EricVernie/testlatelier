using System.Text.Json;

public class ServicePlayers
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private Rootobject _rootobject;

    public ServicePlayers(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }   
    
    public async Task<Rootobject> GetPlayersAsync()
    {
        //TODO : Add try catch
        //TODO : Add logging
        //TODO : Add caching
        //TODO : Test null values
        if (_rootobject != null)
        {
            return _rootobject;
        }
        return _rootobject = await internalGetPlayersAsync();

    }
    public async Task<Player> GetPlayerByIdAsync(string id)
    {
        if (_rootobject == null)
        {
            _rootobject = await internalGetPlayersAsync();
        }
        Player? player=_rootobject.players.Find(p => p.id == Convert.ToInt32(id));
        return player;
    }
    private async Task<Rootobject> internalGetPlayersAsync()
    {
        var HeadToHeadUrl = _configuration["headtoheadurl"];
        var Response = await _httpClient.GetAsync(HeadToHeadUrl);
        Response.EnsureSuccessStatusCode();
        var JsonContent = await Response.Content.ReadAsStringAsync();
        _rootobject = JsonSerializer.Deserialize<Rootobject>(JsonContent);
        return _rootobject;
    }

}