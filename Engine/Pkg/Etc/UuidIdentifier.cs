using StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

namespace StreamlineEngine.Engine.Pkg.Etc;

public class UuidIdentifier
{
  protected List<string> OnceSaid { get; } = [];
  public string Uuid { get; protected set; }
  public string ShortUuid { get; protected set; }

  public UuidIdentifier()
  {
    Uuid = Guid.NewGuid().ToString();
    ShortUuid = Uuid[new Range(0, Defaults.ShortUuidLength)] + ".." + Uuid[new Range(Uuid.Length - Defaults.ShortUuidLength, Uuid.Length)];
  }

  private void Print(string prefix, ConsoleColor color, string message, bool once)
  {
    if (!OnceSaid.Contains(message))
    {
      Console.ForegroundColor = color;
      Console.WriteLine($"{prefix.ToUpper()} '{ShortUuid}': " + message);
      Console.ResetColor();
    }
    if (once && !OnceSaid.Contains(message)) OnceSaid.Add(message);
  }

  protected T Warning<T>(T component, string message, bool once = false)
  {
    Print("warn", ConsoleColor.Yellow, message, once);
    return component;
  }
  protected void Warning(string message, bool once = false)
  {
    Print("warn", ConsoleColor.Yellow, message, once);
  }
  
  protected T Error<T>(T component, string message, bool once = false)
  {
    Print("error", ConsoleColor.Red, message, once);
    return component;
  }
  protected void Error(string message, bool once = false)
  {
    Print("error", ConsoleColor.Red, message, once);
  }
  
  protected T Critical<T>(T component, string message, bool once = false)
  {
    Print("crit", ConsoleColor.DarkRed, message, once);
    return component;
  }
  protected void Critical(string message, bool once = false)
  {
    Print("crit", ConsoleColor.DarkRed, message, once);
  }
}