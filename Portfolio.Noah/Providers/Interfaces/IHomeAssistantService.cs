using Portfolio.Noah.Models.Configuration;

namespace Portfolio.Noah.Providers.Interfaces;

public interface IHomeAssistantService
{
    ValueTask<string> GetStateAsync(string entityId);
}
