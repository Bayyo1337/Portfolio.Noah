using Microsoft.Extensions.Options;
using Portfolio.Noah.Models.Configuration;
using Portfolio.Noah.Models.Experiences;
using Portfolio.Noah.Providers.Interfaces;

namespace Portfolio.Noah.Providers;

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
