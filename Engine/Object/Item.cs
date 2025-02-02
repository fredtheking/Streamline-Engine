using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Object;

public class Item : UuidIdentifier, IScript, ICloneable<Item>
{
  public string Name { get; private set; }
  public List<Node.Folder> Parent { get; private set; } = [];
  public bool Active { get; set; } = true;
  public List<ComponentTemplate> ComponentsList { get; } = [];
  public List<IMaterial> MaterialsList { get; } = [];
  public List<(InitType, Action<Item>)> LateInitActions { get; } = [];
  public List<(InitType, Action<Item>)> EarlyInitActions { get; } = [];

  public Item(string name, params ComponentTemplate[] components)
  {
    Name = name;
    AddComponent(components.ToList());
  }
  
  /// <summary>
  /// First component found in item
  /// </summary>
  /// <typeparam name="T">Type of component</typeparam>
  /// <returns></returns>
  public T? Component<T>() where T : ComponentTemplate =>
    ComponentsList.FirstOrDefault(obj => obj is T) as T;
  /// <summary>
  /// All components found in item
  /// </summary>
  /// <typeparam name="T">Type of component</typeparam>
  /// <returns></returns>
  public T[]? Components<T>() where T : ComponentTemplate =>
    ComponentsList.Where(obj => obj is T) as T[];
  /// <summary>
  /// First material found in item
  /// </summary>
  /// <typeparam name="T">Type of component</typeparam>
  /// <returns></returns>
  public T? Material<T>() where T : class, IMaterial =>
    MaterialsList.FirstOrDefault(mat => mat is T) as T;
  /// <summary>
  /// All materials found in item
  /// </summary>
  /// <typeparam name="T">Type of component</typeparam>
  /// <returns></returns> 
  public T[]? Materials<T>() where T : class, IMaterial =>
    MaterialsList.Where(mat => mat is T) as T[];

  public void AddComponent(params List<ComponentTemplate> component)
  {
    foreach (var c in component.Where(c => !ComponentsList.Contains(c))) ComponentsList.Add(c);
  }
  
  public void AddMaterial(params List<IMaterial> material)
  {
    foreach (var m in material.Where(m => !MaterialsList.Contains(m))) MaterialsList.Add(m);
  }

  public void AddLateInit(InitType type, Action<Item> action) =>
    LateInitActions.Add((type, action));
  
  public void AddEarlyInit(InitType type, Action<Item> action) =>
    EarlyInitActions.Add((type, action));

  public void LocalLatePosSizeInit(dynamic component, bool pos = true, bool size = true)
  {
    if (pos) AddLateInit(InitType.Component, (obj) => component.LocalPosition = new PositionComponent(0));
    if (size) AddLateInit(InitType.Component, (obj) => component.LocalSize = new SizeComponent(0));
  }
  
  public void Init(Context context)
  {
    foreach (var p in EarlyInitActions.OrderBy(p => p.Item1)) p.Item2(this);
    foreach (ComponentTemplate c in ComponentsList) c.Init(context);
    foreach (var p in LateInitActions.OrderBy(p => p.Item1)) p.Item2(this);
  }

  public void Enter(Context context)
  {
    foreach (IMaterial m in MaterialsList) m.Enter(context);
    foreach (ComponentTemplate c in ComponentsList) c.Enter(context);
  }
  
  public void Leave(Context context)
  {
    foreach (IMaterial m in MaterialsList) m.Leave(context);
    foreach (ComponentTemplate c in ComponentsList) c.Leave(context);
  }
  public void Update(Context context)
  {
    foreach (ComponentTemplate c in ComponentsList) c.Update(context);
  }
  public void Draw(Context context)
  {
    foreach (ComponentTemplate component in ComponentsList)
      component.Draw(context);
  }
  
  public Item Clone() => (Item)MemberwiseClone();
}