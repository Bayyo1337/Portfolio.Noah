using Portfolio.Marvin.Terminals.Interfaces;

namespace Portfolio.Marvin.Terminals.Commands;

public sealed class NoahCommand : ITerminalCommand
{
   public string Name => "noah";
   public string Description => "It is noah...";

   public bool IsPublic => true;
   
   public ValueTask Execute(TerminalContext context)
   {
      context.Add("It's one of my best friends!");
      
      return ValueTask.CompletedTask;
   }
}