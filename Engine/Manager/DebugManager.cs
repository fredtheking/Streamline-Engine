namespace StreamlineEngine.Engine.Manager;

public class DebugManager
{
  public bool TurnedOn { get; set; } = Config.DebugMode;
  public bool Changed { get; set; } = true;
  
  public void PrintSeparator(ConsoleColor fgColor = ConsoleColor.Magenta, string message = "") {
    string space = " ";
    if (message is "") space = "";
    string prepostfix = new('=', (Console.WindowWidth - message.Length - space.Length * 2) / 2);
    
    Console.ForegroundColor = fgColor;
    Console.Write(prepostfix + space + message + space + prepostfix);
    Console.ResetColor();
    Console.WriteLine();
  }
  
  public void PrintSeparator(string message) => PrintSeparator(ConsoleColor.Magenta, message);
  
  private void Print(string prefix, ConsoleColor? backColor, ConsoleColor foreColor, string message)
  {
    if (backColor is not null) Console.BackgroundColor = (ConsoleColor)backColor;
    Console.ForegroundColor = foreColor;
    Console.Write($"{prefix.ToUpper()}: {message}");
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