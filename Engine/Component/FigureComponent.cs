using ImGuiNET;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class FigureComponent : ComponentTemplate
{
  public FigureType Type { get; set; }
  public float Roundness { get; set; }
  private bool TypeInit { get; set; }
  
  public FigureComponent() { TypeInit = true; Roundness = 1f; } 
  public FigureComponent(float roundness) { TypeInit = true; Roundness = roundness; } 
  public FigureComponent(FigureType type) { Type = type; Roundness = 1f; } 
  public FigureComponent(FigureType type, float roundness) { Type = type; Roundness = roundness; }

  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      if (!TypeInit) return;
      Type = Defaults.Figure;
    });
  }

  public override void DebuggerInfo(Context context)
  {
    base.DebuggerInfo(context);
    ImGui.Text($"Type: {Type}");
    if (Type == FigureType.Rounded) ImGui.Text($"Roundness: {Roundness}");
  }
}