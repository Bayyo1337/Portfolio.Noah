using Portfolio.Noah.Models.Configuration;
using Portfolio.Noah.Models.Projects;
using Portfolio.Noah.Providers.Interfaces;
using Microsoft.Extensions.Options;

namespace Portfolio.Noah.Providers;

public sealed class ProjectProvider : IProjectProvider
{
    private readonly PortfolioConfiguration _configuration;

    public ProjectProvider(IOptions<PortfolioConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public IEnumerable<Project> GetAllProjects()
    {
        return _configuration.Projects
            .Select(p => new Project
            {
                Name = p.Name,
                StartedAt = p.StartedAt,
                ProjectUrl = p.Url,
                ImageUrls = p.ImageUrls,
                Descriptions = p.DescriptionPoints,
                SkillIds = p.SkillIds
            })
            .OrderByDescending(x => x.StartedAt);
    }
}
