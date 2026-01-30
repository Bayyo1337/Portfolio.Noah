using Portfolio.Noah.Models.Configuration;

namespace Portfolio.Noah.Models.Experiences;

public class Experience
{
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyImageUrl { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public DateTimeOffset StartedAt { get; set; }
    public DateTimeOffset? EndedAt { get; set; }
    public List<string> ImageUrls { get; set; } = []; // Kept for compatibility if needed, though config doesn't use it yet
    public List<string> SkillIds { get; set; } = [];
    public List<string> Descriptions { get; set; } = [];
}
