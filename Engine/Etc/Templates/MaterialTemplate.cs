using ImGuiNET;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Etc.Templates;

public abstract class MaterialTemplate : UuidIdentifier
{
  public int Id { get; protected set; }
  public dynamic? Material { get; protected set; }
  public bool LoadOnNeed { get; protected init; }
  public abstract bool Ready();
  
  public abstract void Load(Context context);
  public abstract void Unload(Context context);
  public virtual void Init(Context context) { InitOnce(() => {}); }

  public virtual void Enter(Context context)
  {
    if (Ready() || LoadOnNeed) return;
    Load(context);
  }

  public virtual void Leave(Context context)
  {
    if (!Ready()) return;
    Unload(context);
  }
  public void Update(Context context) =>
    throw new CallNotAllowedException("Should not be called");
  public void Draw(Context context) =>
    throw new CallNotAllowedException("Should not be called");
  
  #if !RESOURCES
  public override void DebuggerInfo(Context context)
  {
    Item parent = context.Managers.Object.GetByMaterial(this);
    
    if (ImGui.SmallButton("Back"))
      context.Debugger.CurrentTreeInfo.RemoveAt(context.Debugger.CurrentTreeInfo.Count - 1);
    ImGui.SameLine();
    ImGui.Text("to parent Item");
    
    if (ImGui.SmallButton(parent.ShortUuid))
      context.Debugger.CurrentTreeInfo.Add(parent.DebuggerInfo);
    ImGui.SameLine();
    ImGui.Text($"Parent Item: {parent.Name}");
    ImGui.Separator();
    base.DebuggerInfo(context);
    ImGui.Text($"Loaded: {(Ready() ? "Yes" : "No")}");
  }
  #endif
}