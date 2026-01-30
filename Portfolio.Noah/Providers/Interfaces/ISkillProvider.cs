using Portfolio.Noah.Models.Configuration;

namespace Portfolio.Noah.Providers.Interfaces;

public interface ISkillProvider
{
    SkillConfig? GetSkill(string id);
    IEnumerable<SkillConfig> GetAllSkills();
}
