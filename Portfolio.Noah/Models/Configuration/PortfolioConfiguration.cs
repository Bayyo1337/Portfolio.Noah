using System.Text.Json.Serialization;

namespace Portfolio.Noah.Models.Configuration;

public class PortfolioConfiguration
{
    public GeneralInfo GeneralInfo { get; set; } = new();
    public SectionSettings SectionSettings { get; set; } = new();
    public HomeAssistantSettings HomeAssistant { get; set; } = new();
    public List<SocialLink> SocialLinks { get; set; } = [];
    public List<SkillConfig> Skills { get; set; } = [];
    public List<ExperienceConfig> Experiences { get; set; } = [];
    public List<ProjectConfig> Projects { get; set; } = [];
}

public class GeneralInfo
{
    public string Name { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string CurrentEmployer { get; set; } = string.Empty;
    public string CurrentEmployerLogoUrl { get; set; } = string.Empty;
}

public class SectionSettings
{
    public bool ShowTerminal { get; set; } = true;
    public bool ShowExperiences { get; set; } = true;
    public bool ShowProjects { get; set; } = true;
    public bool ShowContact { get; set; } = true;
    public bool ShowBlogs { get; set; } = true;
    public bool ShowHomeAssistant { get; set; } = false;
}

public class HomeAssistantSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public string TokenEnvVar { get; set; } = "PORTFOLIO_HA_TOKEN";
    public List<SensorConfig> Sensors { get; set; } = [];
}

public class SensorConfig
{
    public string EntityId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty; // Material Design Icon name or SVG path
    public string Unit { get; set; } = string.Empty;
}

public class SocialLink
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string DisplayText { get; set; } = string.Empty;
    public string IconName { get; set; } = string.Empty;
}

public class SkillConfig
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string Color { get; set; } = "#ffffff";
    public bool IsDark { get; set; }
}

public class ExperienceConfig
{
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyImageUrl { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public DateTimeOffset StartedAt { get; set; }
    public DateTimeOffset? EndedAt { get; set; }
    public List<string> DescriptionPoints { get; set; } = [];
    public List<string> SkillIds { get; set; } = [];
}

public class ProjectConfig
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public DateTimeOffset StartedAt { get; set; }
    public List<string> ImageUrls { get; set; } = [];
    public List<string> DescriptionPoints { get; set; } = [];
    public List<string> SkillIds { get; set; } = [];
}
