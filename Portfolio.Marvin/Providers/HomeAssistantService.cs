using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Portfolio.Marvin.Models.Configuration;
using Portfolio.Marvin.Providers.Interfaces;

namespace Portfolio.Marvin.Providers;

public class HomeAssistantService : IHomeAssistantService
{
    private readonly HttpClient _httpClient;
    private readonly PortfolioConfiguration _config;

    public HomeAssistantService(HttpClient httpClient, IOptions<PortfolioConfiguration> config)
    {
        _httpClient = httpClient;
        _config = config.Value;

        var token = Environment.GetEnvironmentVariable(_config.HomeAssistant.TokenEnvVar);

        if (!string.IsNullOrEmpty(_config.HomeAssistant.BaseUrl) && !string.IsNullOrEmpty(token))
        {
            _httpClient.BaseAddress = new Uri(_config.HomeAssistant.BaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async ValueTask<string> GetStateAsync(string entityId)
    {
        try
        {
            // Simple GET /api/states/{entity_id}
            var response = await _httpClient.GetAsync($"api/states/{entityId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("state", out var stateProp))
                {
                    return stateProp.GetString() ?? "Unknown";
                }
            }
        }
        catch
        {
            // Ignore errors, return default
        }
        return "Unavailable";
    }
}
