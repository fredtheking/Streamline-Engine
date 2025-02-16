using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Manager;

public class DebugManager
{
  public bool ShowBorders { get; set; } = Config.DebugModeByDefault;
  public bool TurnedOn { get; set; } = Config.DebugModeByDefault;
  public bool Changed { get; set; } = true;
  
  public void Separator(ConsoleColor fgColor = ConsoleColor.Magenta, string message = "", char sign = '=') {
    string space = " ";
    if (message is "") space = "";
    string prepostfix = new(sign, (Console.WindowWidth - message.Length - space.Length * 2) / 2);
    
    Console.ForegroundColor = fgColor;
    Console.Write(prepostfix + space + message + space + prepostfix);
    Console.ResetColor();
    Console.WriteLine();
  }
  
  public void Separator(string message, char sign = '=') => Separator(ConsoleColor.Magenta, message, sign);
  
  public void Print(string prefix, ConsoleColor? backColor, ConsoleColor foreColor, string message, UuidIdentifier? component = null)
  {
    if (backColor is not null) Console.BackgroundColor = (ConsoleColor)backColor;
    Console.ForegroundColor = foreColor;
    Console.Write($"{prefix.ToUpper()}{(component is not null ? $" [{component.ShortUuid}]": "")}: {message}");
    Console.ResetColor();
    Console.WriteLine();
  }
  
  public void Information(string message) =>
    Print("info", null, ConsoleColor.White, message);
 
  public void Warning(string message) =>
    Print("warn", null, ConsoleColor.Yellow, message);
 
  public void Error(string message) =>
    Print("error", null, ConsoleColor.Red, message);
 
  public void Critical(string message) =>
    Print("crit", ConsoleColor.Red, ConsoleColor.Black, message);
  
  public void Toggle() {
    TurnedOn = !TurnedOn;
    Changed = true;
  }
}