using ImGuiNET;
using Raylib_cs;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Etc.Templates;

public abstract class ComponentTemplate : UuidIdentifier, IScript
{
  public Color DebugBorderColor { get; protected init; } = Color.Pink;
  public virtual void Init(Context context) { InitOnce(() => {}); }
  public virtual void CheckInitCorrect(Context context) { if (!Initialized) throw new NotInitialisedException(); }
  public virtual void Enter(Context context) { }
  public virtual void Leave(Context context) { }
  public virtual void EarlyUpdate(Context context) { }
  public virtual void Update(Context context) { }
  public virtual void LateUpdate(Context context) { }
  public virtual void PreDraw(Context context) { }
  public virtual void Draw(Context context) { }
  public virtual void DebugDraw(Context context) { }
  
  #if !RESOURCES
  public override void DebuggerInfo(Context context)
  {
    Item parent = context.Managers.Object.GetByComponent(this);
    
    if (ImGui.SmallButton("Back"))
      context.Debugger.CurrentTreeInfo.RemoveAt(context.Debugger.CurrentTreeInfo.Count - 1);
    ImGui.SameLine();
    ImGui.Text("to parent Item");
    
    if (ImGui.SmallButton(parent.ShortUuid))
      context.Debugger.CurrentTreeInfo.Add(parent.DebuggerInfo);
    ImGui.SameLine();
    ImGui.Text($"Original Item: {parent.Name}");
    ImGui.Separator();
    base.DebuggerInfo(context);
  }
  #endif
}