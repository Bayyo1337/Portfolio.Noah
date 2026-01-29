using Microsoft.Extensions.Options;
using Portfolio.Marvin.Models.Configuration;
using Portfolio.Marvin.Models.Experiences;
using Portfolio.Marvin.Providers.Interfaces;

namespace Portfolio.Marvin.Providers;

public sealed class ExperienceProvider : IExperienceProvider
{
    private readonly PortfolioConfiguration _configuration;

    public ExperienceProvider(IOptions<PortfolioConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public IEnumerable<Experience> GetAllExperiences()
    {
        return _configuration.Experiences
            .Select(e => new Experience
            {
                CompanyName = e.CompanyName,
                CompanyImageUrl = e.CompanyImageUrl,
                JobTitle = e.JobTitle,
                StartedAt = e.StartedAt,
                EndedAt = e.EndedAt == DateTimeOffset.MaxValue ? null : e.EndedAt,
                Descriptions = e.DescriptionPoints,
                SkillIds = e.SkillIds
            })
            .OrderByDescending(x => x.EndedAt ?? DateTimeOffset.MaxValue);
    }
}
