using ImGuiNET;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Etc;

public class UuidIdentifier : IDebuggerTreeObject
{
  public bool Initialized { get; protected set; }
  private List<string> OnceSaid { get; } = [];
  public string Uuid { get; protected set; }
  public string ShortUuid { get; protected set; }

  public UuidIdentifier()
  {
    Uuid = Guid.NewGuid().ToString();
    ShortUuid = Uuid[new Range(0, Defaults.ShortUuidLength)] + ".." + Uuid[new Range(Uuid.Length - Defaults.ShortUuidLength, Uuid.Length)];
  }
  
  protected void InitOnce(Action action)
  {
    if (Initialized) return;
    action();
    Initialized = true;
  }

  private void Print(Context context, string prefix, ConsoleColor? backColor, ConsoleColor foreColor, string message, bool once)
  {
    if (!OnceSaid.Contains(message))
      context.Managers.Debug.Print(prefix, backColor, foreColor, message, this);
    if (once && !OnceSaid.Contains(message)) OnceSaid.Add(message);
  }

  protected T Information<T>(Context context, T @return, string message, bool once = false)
  {
    Information(context, message, once);
    return @return;
  }

  protected void Information(Context context, string message, bool once = false) =>
    Print(context, "info", null, ConsoleColor.Gray, message, once);
  
  protected T Warning<T>(Context context, T @return, string message, bool once = false)
  {
    Warning(context, message, once);
    return @return;
  }
  protected void Warning(Context context, string message, bool once = false) =>
    Print(context, "warn", null, ConsoleColor.Yellow, message, once);
  
  protected T Error<T>(Context context, T @return, string message, bool once = false)
  {
    Error(context, message, once);
    return @return;
  }
  protected void Error(Context context, string message, bool once = false) =>
    Print(context, "error", null, ConsoleColor.Red, message, once);
  
  protected T Critical<T>(Context context, T @return, string message, bool once = false)
  {
    Critical(context, message, once);
    return @return;
  }
  protected void Critical(Context context, string message, bool once = false) =>
    Print(context, "crit", ConsoleColor.Red, ConsoleColor.Black, message, once);

  public virtual void DebuggerTree(Context context) { }

  public virtual void DebuggerInfo(Context context)
  {
    ImGui.Text($"TypeOf: {GetType().Name}");
    ImGui.Text($"UUID: {Uuid}");
    ImGui.Text($"Short UUID: {ShortUuid}");
    ImGui.Text($"Initialized: {(Initialized ? "Yes" : "No")}");
    ImGui.Separator();
  }
}