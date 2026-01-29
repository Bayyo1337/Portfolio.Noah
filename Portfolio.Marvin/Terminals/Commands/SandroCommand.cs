using Portfolio.Marvin.Terminals.Interfaces;

namespace Portfolio.Marvin.Terminals.Commands;

public sealed class SandroCommand : ITerminalCommand
{
   public string Name => "sandro";
   public string Description => "It is Sandro...";

   public bool IsPublic => true;
   
   public ValueTask Execute(TerminalContext context)
   {
      context.Add("It's Sandro, my partner for many projects!");
      context.Add("He is a very good vibe coder!");
      
      return ValueTask.CompletedTask;
   }
}