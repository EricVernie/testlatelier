using System.Text.Json;

public class ServicePlayers
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public ServicePlayers(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }   
    
    public async Task<Rootobject> GetPlayerAsync()
    {
        //TODO : Add try catch
        //TODO : Add logging
        //TODO : Add caching
        //TODO : Test null values

        var HeadToHeadUrl = _configuration["headtoheadurl"];
        var Response = await _httpClient.GetAsync(HeadToHeadUrl);
        Response.EnsureSuccessStatusCode();
        var JsonContent = await Response.Content.ReadAsStringAsync();
        var Players = JsonSerializer.Deserialize<Rootobject>(JsonContent);
        return Players;
    }
}