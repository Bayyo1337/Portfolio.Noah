using Portfolio.Noah.Providers;
using Portfolio.Noah.Providers.Interfaces;
using Portfolio.Noah.Terminals;
using Portfolio.Noah.Terminals.Commands;
using Portfolio.Noah.Terminals.Interfaces;

namespace Portfolio.Noah.Extensions;

public static class ServiceCollectionExtensions
{
   public static IServiceCollection AddPortfolioServices(this IServiceCollection services, IConfiguration configuration)
   {
      return services
         .AddProviders(configuration)
         .AddTerminalCommands(configuration);
   }

   public static IServiceCollection AddProviders(this IServiceCollection services, IConfiguration configuration)
   {
      return services
         .AddSingleton<ISkillProvider, SkillProvider>()
         .AddSingleton<IExperienceProvider, ExperienceProvider>()
         .AddSingleton<IProjectProvider, ProjectProvider>()
         .AddSingleton<IBlogProvider, BlogProvider>()
         .AddHttpClient<IHomeAssistantService, HomeAssistantService>().Services;
   }

   public static IServiceCollection AddTerminalCommands(this IServiceCollection services, IConfiguration configuration)
   {
      return services
         .AddSingleton<TerminalCommandHandler>()
         .AddSingleton<TerminalCommandRegistry>()
         .AddSingleton<TerminalPublicCommands>()
         .AddSingleton<ITerminalCommand, HelpCommand>()
         .AddSingleton<ITerminalCommand, SkillsCommand>()
         .AddSingleton<ITerminalCommand, SudoCommand>();
   }
}
