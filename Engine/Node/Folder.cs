using System.Numerics;
using ImGuiNET;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Node;

public class Folder : UuidIdentifier, IFolder<object>, IScript
{
  public string Name { get; set; }
  public bool Active { get; set; } = true;
  public FolderNodeType Type { get; set; }
  public List<dynamic>? Parent { get; set; } = [];
  public List<dynamic>? Children { get; set; }
  
  public Folder(string name, FolderNodeType type, params dynamic[] children) {
    Name = name;
    Type = type;
    Children = children.ToList();
  }

  public void Init(Context context)
  {
    InitOnce(() =>
    {
      foreach (dynamic child in Children ?? [])
      {
        child.Init(context);
        child.Parent.Add(this);
      }
    });
  }
  public void CheckInitCorrect(Context context) { if (!Initialized) throw new NotInitialisedException(); }
  public void Enter(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
      child.Enter(context);
  }
  public void Leave(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
      child.Leave(context);
  }
  public void EarlyUpdate(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
    {
      if (child is Item && child.Type == ItemObjectType.Static) continue;
      child.EarlyUpdate(context);
    }
  }
  public void Update(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
    {
      if (child is Item && child.Type == ItemObjectType.Static) continue;
      child.Update(context);
    }
  }
  public void LateUpdate(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
    {
      if (child is Item && child.Type == ItemObjectType.Static) continue;
      child.LateUpdate(context);
    }
  }
  public void Draw(Context context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active) ?? [])
    {
      if (child is Item) context.Managers.Render.CurrentFrame.Add(child);
      else child.Draw(context);
    }
  }

  public override void DebuggerTree(Context context)
  {
    ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1f));
    
    if (ImGui.SmallButton(ShortUuid))
      context.Debugger.CurrentTreeInfo.Add(DebuggerInfo);
    ImGui.SameLine();
    
    DefineColor();
    if (ImGui.TreeNode(Name)){
      foreach (dynamic child in Children ?? [])
        child.DebuggerTree(context);
      ImGui.TreePop();
    }
    
    ImGui.PopStyleColor(5); 
  }

  private void DefineColor()
  {
    switch (Type)
    {
      case FolderNodeType.Scene:
        ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(.5f, .9f, 1f, 1f));
        ImGui.PushStyleColor(ImGuiCol.Header, new Vector4(0f, .4f, .8f, 1f));
        ImGui.PushStyleColor(ImGuiCol.HeaderHovered, new Vector4(.2f, .4f, .7f, 1f));
        ImGui.PushStyleColor(ImGuiCol.HeaderActive, new Vector4(0.0f, 0.6f, 1.0f, 1.0f));

        break;
      case FolderNodeType.Node:
        ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1f, .4f, .4f, 1f));
        ImGui.PushStyleColor(ImGuiCol.Header, new Vector4(.4f, .4f, 1f, 1f));
        ImGui.PushStyleColor(ImGuiCol.HeaderHovered, new Vector4(.4f, .1f, .1f, 1f));
        ImGui.PushStyleColor(ImGuiCol.HeaderActive, new Vector4(.5f, .4f, .3f, 1f));
        break;
      case FolderNodeType.Item:
        ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(0f, 1f, 0f, 1f));
        ImGui.PushStyleColor(ImGuiCol.Header, new Vector4(0f, .8f, 0f, 1f));
        ImGui.PushStyleColor(ImGuiCol.HeaderHovered, new Vector4(.2f, .6f, .2f, 1f));
        ImGui.PushStyleColor(ImGuiCol.HeaderActive, new Vector4(0f, .7f, 0f, 1f));

        break;
    }
  }

  public override void DebuggerInfo(Context context)
  {
    ImGui.Text($"Name: {Name}");
    base.DebuggerInfo(context);
    ImGui.Text($"TypeOf: {GetType().Name}");
    ImGui.Text($"Subtype: {Type}");
    ImGui.Separator();
    
  }
}