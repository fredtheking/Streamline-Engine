using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.FolderItem;

public class Folder : UuidIdentifier, IScript, ICloneable<Folder>
{
  public string Name { get; private set; }
  public bool Active { get; set; } = true;
  public List<Folder>? Parent { get; private set; } = [];
  public List<dynamic>? Children { get; private set; }
  
  public Folder(string name, params dynamic[] children) => (Name, Children) = (name, children?.ToList());
  
  public void Add(dynamic child) => Children?.Add(child);

  public virtual void Init(MainContext context)
  {
    foreach (dynamic child in Children ?? []) child.Parent.Add(this);
    foreach (Folder child in Children?.OfType<Folder>() ?? []) child.Init(context);
  }

  public virtual void Enter(MainContext context)
  {
    foreach (dynamic child in Children ?? []) child.Enter(context);
  }

  public virtual void Leave(MainContext context)
  {
    foreach (dynamic child in Children ?? []) child.Leave(context);
  }

  public virtual void Update(MainContext context)
  {
    foreach (dynamic child in Children ?? [])
    {
      if (!child.Active) continue;
      child.Update(context);
    }
  }

  public virtual void Draw(MainContext context)
  {
    foreach (dynamic child in Children?.Where(c => c.Active).ToList() ?? []) child.Draw(context);
  }
  
  public Folder Clone() => (Folder)MemberwiseClone();
}