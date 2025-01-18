using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Folder;

public class FolderNode : UuidIdentifier, IFolder<object>, IScript
{
  public string Name { get; set; }
  public bool Active { get; set; }
  public FolderNodeType Type { get; set; }
  public List<dynamic>? Parent { get; set; }
  public List<dynamic>? Children { get; set; }
  
  public FolderNode(string name, FolderNodeType type, params dynamic[] children) {
    Name = name;
    Type = type;
    Children = children.ToList();
  }

  public void Init(Context context)
  {
    foreach (dynamic child in Children ?? [])
      child.Init(context);
  }
  public void Enter(Context context)
  {
    foreach (dynamic child in Children ?? [])
      child.Enter(context);
  }
  public void Leave(Context context)
  {
    foreach (dynamic child in Children ?? [])
      child.Leave(context);
  }
  public void Update(Context context)
  {
    foreach (dynamic child in Children ?? [])
      child.Update(context);
  }
  public void Draw(Context context)
  {
    foreach (dynamic child in Children ?? [])
      child.Draw(context);
  }
}