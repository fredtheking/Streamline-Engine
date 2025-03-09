using ImGuiNET;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class LayerComponent : ComponentTemplate
{
  public RefObj<int> Layer { get; set; }
  
  public LayerComponent(int layer = 0) => Layer = new RefObj<int>(layer);

  public override void DebuggerInfo(Context context)
  {
    base.DebuggerInfo(context);
    ImGui.Text($"Layer: {Layer.Value}");
  }
}