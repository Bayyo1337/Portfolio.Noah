using Portfolio.Marvin.Models.Configuration;

namespace Portfolio.Marvin.Providers.Interfaces;

public interface ISkillProvider
{
    SkillConfig? GetSkill(string id);
    IEnumerable<SkillConfig> GetAllSkills();
}
