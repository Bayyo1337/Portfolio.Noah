using Portfolio.Noah.Models.Projects;

namespace Portfolio.Noah.Providers.Interfaces;

public interface IProjectProvider
{
   public IEnumerable<Project> GetAllProjects();
}