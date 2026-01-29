using Portfolio.Marvin.Models.Blogs;

namespace Portfolio.Marvin.Models.Blogs;

public class BlogPageMeta
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string RelativeUrl { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string ModelUrl { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = [];
    public List<string> SkillIds { get; set; } = [];
}
