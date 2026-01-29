using Microsoft.Extensions.Options;
using Portfolio.Marvin.Models.Configuration;
using Portfolio.Marvin.Providers.Interfaces;

namespace Portfolio.Marvin.Providers;

public sealed class SkillProvider : ISkillProvider
{
    private readonly PortfolioConfiguration _configuration;
    private readonly Dictionary<string, SkillConfig> _skills;

    public SkillProvider(IOptions<PortfolioConfiguration> configuration)
    {
        _configuration = configuration.Value;
        _skills = _configuration.Skills.ToDictionary(s => s.Id, s => s, StringComparer.OrdinalIgnoreCase);
    }

    public SkillConfig? GetSkill(string id)
    {
        return _skills.GetValueOrDefault(id);
    }

    public IEnumerable<SkillConfig> GetAllSkills()
    {
        return _configuration.Skills;
    }
}
