using Portfolio.Noah.Terminals.Interfaces;

namespace Portfolio.Noah.Terminals;

public sealed class TerminalPublicCommands
{
   public Dictionary<string, ITerminalCommand> Commands { get; } = [];

   public TerminalPublicCommands(IEnumerable<ITerminalCommand> commands)
   {
      foreach (var command in commands.Where(x => x.IsPublic))
      {
         Commands[command.Name] = command;
      }
   }
}