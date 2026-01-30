using Portfolio.Noah.Models.Experiences;

namespace Portfolio.Noah.Providers.Interfaces;

public interface IExperienceProvider
{
   public IEnumerable<Experience> GetAllExperiences();
}