using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Folder;

namespace StreamlineEngine.Engine.Item;

public class Item : UuidIdentifier, IScript, ICloneable<Item>
{
  public string Name { get; private set; }
  public List<FolderNode> Parent { get; private set; } = [];
  public bool Active { get; set; } = true;
  public List<ComponentTemplate> Components { get; } = [];
  public List<IMaterial> Materials { get; } = [];
  public List<(LateInitType, Action)> LateInitActions { get; } = [];

  public Item(string name, params ComponentTemplate[] components)
  {
    Name = name;
    AddComponent(components.ToList());
  }
  
  public T? Component<T>() where T : ComponentTemplate
  {
    foreach (ComponentTemplate c in Components)
    {
      if (c is T template) return template;
    }
    return null;
  }
  
  public T? Material<T>() where T : class, IMaterial
  {
    foreach (IMaterial c in Materials)
    {
      if (c is T template) return template;
    }
    return null;
  }

  public void AddComponent(params List<ComponentTemplate> component)
  {
    foreach (var c in component.Where(c => !Components.Contains(c))) Components.Add(c);
  }
  
  public void AddMaterial(params List<IMaterial> material)
  {
    foreach (var m in material.Where(m => !Materials.Contains(m))) Materials.Add(m);
  }

  public void AddLateInit(LateInitType type, Action action) =>
    LateInitActions.Add((type, action));

  public void LocalLateInit(dynamic component, bool pos = true, bool size = true)
  {
    if (pos) AddLateInit(LateInitType.Component, () => component.LocalPosition = new PositionComponent(0));
    if (size) AddLateInit(LateInitType.Component, () => component.LocalSize = new SizeComponent(0));
  }
  
  public void Init(Context context)
  {
    foreach (ComponentTemplate c in Components) c.Init(context);
    foreach (var p in LateInitActions.OrderBy(p => p.Item1)) p.Item2();
  }

  public void Enter(Context context)
  {
    foreach (IMaterial m in Materials) m.Enter(context);
    foreach (ComponentTemplate c in Components) c.Enter(context);
  }
  
  public void Leave(Context context)
  {
    foreach (IMaterial m in Materials) m.Leave(context);
    foreach (ComponentTemplate c in Components) c.Leave(context);
  }
  public void Update(Context context)
  {
    foreach (ComponentTemplate c in Components) c.Update(context);
  }
  public void Draw(Context context)
  {
    foreach (ComponentTemplate component in Components)
      component.Draw(context);
  }
  
  public Item Clone() => (Item)MemberwiseClone();
}