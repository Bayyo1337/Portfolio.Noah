using Portfolio.Noah.Terminals.Interfaces;

namespace Portfolio.Noah.Terminals.Commands;

public sealed class SudoCommand : ITerminalCommand
{
   public string Name => "sudo";
   public string Description => "";
   
   public bool IsPublic => false;

   public ValueTask Execute(TerminalContext context)
   {
      context.Add($"<span style='color: var(--error-color)'>👀 Nice try. You’re not even root in your own life.</span>");
      
      return ValueTask.CompletedTask;
   }
}