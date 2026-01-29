namespace Portfolio.Marvin.Models.Projects;

public class Project
{
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset StartedAt { get; set; }
    public string ProjectUrl { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = [];
    public List<string> Descriptions { get; set; } = [];
    public List<string> SkillIds { get; set; } = [];
}
