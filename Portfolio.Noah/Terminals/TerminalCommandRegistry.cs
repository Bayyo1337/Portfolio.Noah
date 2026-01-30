using Portfolio.Noah.Terminals.Commands;
using Portfolio.Noah.Terminals.Interfaces;

namespace Portfolio.Noah.Terminals;

public sealed class TerminalCommandRegistry
{
   private readonly Dictionary<string, ITerminalCommand> _commands = new (StringComparer.OrdinalIgnoreCase);

   public TerminalCommandRegistry(IEnumerable<ITerminalCommand> commands)
   {
      foreach (var command in commands)
      {
         _commands[command.Name] = command;
      }
   }
   
   public ITerminalCommand? Get(string name)
   {
      return _commands.GetValueOrDefault(name);
   }
}