namespace StreamlineEngine.Engine.Etc;

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

  private void Print(string prefix, ConsoleColor? backColor, ConsoleColor foreColor, string message, bool once)
  {
    if (!OnceSaid.Contains(message))
    {
      if (backColor is not null) Console.BackgroundColor = (ConsoleColor)backColor;
      Console.ForegroundColor = foreColor;
      Console.Write($"{prefix.ToUpper()} '{ShortUuid}': " + message);
      Console.ResetColor();
      Console.WriteLine();
    }
    if (once && !OnceSaid.Contains(message)) OnceSaid.Add(message);
  }

  protected T Information<T>(T @return, string message, bool once = false)
  {
    Print("info", null, ConsoleColor.White, message, once);
    return @return;
  }
  protected void Information(string message, bool once = false) =>
    Print("info", null, ConsoleColor.White, message, once);
  
  protected T Warning<T>(T @return, string message, bool once = false)
  {
    Print("warn", null, ConsoleColor.Yellow, message, once);
    return @return;
  }
  protected void Warning(string message, bool once = false) =>
    Print("warn", null, ConsoleColor.Yellow, message, once);
  
  protected T Error<T>(T @return, string message, bool once = false)
  {
    Print("error", null, ConsoleColor.Red, message, once);
    return @return;
  }
  protected void Error(string message, bool once = false) =>
    Print("error", null, ConsoleColor.Red, message, once);
  
  protected T Critical<T>(T @return, string message, bool once = false)
  {
    Print("crit", ConsoleColor.Red, ConsoleColor.Black, message, once);
    return @return;
  }
  protected void Critical(string message, bool once = false) =>
    Print("crit", ConsoleColor.Red, ConsoleColor.Black, message, once);
}