using Portfolio.Marvin.Models.Configuration;

namespace Portfolio.Marvin.Providers.Interfaces;

public interface IHomeAssistantService
{
    ValueTask<string> GetStateAsync(string entityId);
}
