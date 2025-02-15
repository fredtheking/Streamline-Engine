using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Node;

namespace StreamlineEngine.Engine.Object;

public class Item : UuidIdentifier, IScript, ICloneable<Item>
{
  public string Name { get; private set; }
  public List<Folder> Parent { get; private set; } = [];
  public bool Active { get; set; } = true;
  public ItemObjectType Type { get; set; }
  public List<ComponentTemplate> ComponentsList { get; } = [];
  public List<MaterialTemplate> MaterialsList { get; } = [];
  public List<(InitType, Action<Item>)> LateInitActions { get; } = [];
  public List<(InitType, Action<Item>)> EarlyInitActions { get; } = [];

  public Item(string name, ItemObjectType type, params ComponentTemplate[] components)
  {
    Name = name;
    Type = type;
    AddComponents(components.ToList());
  }
  
  /// <summary>
  /// First component found in item
  /// </summary>
  /// <typeparam name="T">Type of component</typeparam>
  public T Component<T>() where T : ComponentTemplate =>
    (ComponentsList.First(obj => obj is T) as T)!;
  /// <summary>
  /// First component found in item (or <c>null</c>, if did not)
  /// </summary>
  /// <typeparam name="T">Type of component</typeparam>
  public T? ComponentTry<T>() where T : ComponentTemplate =>
    ComponentsList.FirstOrDefault(obj => obj is T) as T;
  /// <summary>
  /// All components found in item
  /// </summary>
  /// <typeparam name="T">Type of component</typeparam>
  public T[] Components<T>() where T : ComponentTemplate =>
    (ComponentsList.Where(obj => obj is T) as T[])!;
  /// <summary>
  /// All components found in item (or <c>null</c>, if did not)
  /// </summary>
  /// <typeparam name="T">Type of component</typeparam>
  public T[]? ComponentsTry<T>() where T : ComponentTemplate =>
    ComponentsList.Where(obj => obj is T) as T[];
  /// <summary>
  /// First material found in item
  /// </summary>
  /// <typeparam name="T">Type of material</typeparam>
  /// <returns></returns>
  public T Material<T>() where T : MaterialTemplate =>
    (MaterialsList.First(mat => mat is T) as T)!;
  /// <summary>
  /// First material found in item (or <c>null</c>, if did not)
  /// </summary>
  /// <typeparam name="T">Type of material</typeparam>
  public T? MaterialTry<T>() where T : MaterialTemplate =>
    MaterialsList.FirstOrDefault(mat => mat is T) as T;
  /// <summary>
  /// All materials found in item
  /// </summary>
  /// <typeparam name="T">Type of material</typeparam>
  /// <returns></returns> 
  public T[] Materials<T>() where T : MaterialTemplate =>
    (MaterialsList.Where(mat => mat is T) as T[])!;
  /// <summary>
  /// All materials found in item (or <c>null</c>, if did not)
  /// </summary>
  /// <typeparam name="T">Type of material</typeparam>
  public T[]? MaterialsTry<T>() where T : MaterialTemplate =>
    MaterialsList.Where(mat => mat is T) as T[];

  public void AddComponents(params List<ComponentTemplate> component) => 
    component.ForEach(c => ComponentsList.Add(c));
  public void RemoveExactComponents(params List<ComponentTemplate> component) => 
    component.ForEach(c => ComponentsList.Remove(c));
  public void RemoveAllComponents<T>() where T : ComponentTemplate => 
    ComponentsList.Where(obj => obj is T).ToList().ForEach(obj => ComponentsList.Remove(obj));

  public void AddMaterials(params List<MaterialTemplate> material) => 
    material.ForEach(m => MaterialsList.Add(m));
  public void RemoveExactMaterials(params List<MaterialTemplate> material) => 
    material.ForEach(m => MaterialsList.Remove(m));
  public void RemoveAllMaterials<T>() where T : MaterialTemplate => 
    MaterialsList.Where(obj => obj is T).ToList().ForEach(obj => MaterialsList.Remove(obj));

  public void AddEarlyInit(InitType type, Action<Item> action) =>
    EarlyInitActions.Add((type, action));

  public void AddLateInit(InitType type, Action<Item> action) =>
    LateInitActions.Add((type, action));

  public void LocalLatePosSizeInit(dynamic component, bool pos = true, bool size = true)
  {
    if (pos) AddLateInit(InitType.Component, obj => component.LocalPosition = new PositionComponent(0));
    if (size) AddLateInit(InitType.Component, obj => component.LocalSize = new SizeComponent(0));
  }
  
  public void Init(Context context)
  {
    InitOnce(() =>
    {
      foreach (var p in EarlyInitActions.OrderBy(p => p.Item1)) p.Item2(this);
      foreach (ComponentTemplate c in ComponentsList) c.Init(context);
      foreach (var p in LateInitActions.OrderBy(p => p.Item1)) p.Item2(this);
      
      context.Managers.Render.All.Add(this, (Draw, ComponentTry<LayerComponent>()?.Layer ?? new RefObj<int>(0)));
    });
  }

  public void CheckInitCorrect(Context context) { if (!Initialized) throw new NotInitialisedException(); }

  public void Enter(Context context)
  {
    foreach (MaterialTemplate m in MaterialsList) m.Enter(context);
    foreach (ComponentTemplate c in ComponentsList) c.Enter(context);
  }
  
  public void Leave(Context context)
  {
    foreach (MaterialTemplate m in MaterialsList) m.Leave(context);
    foreach (ComponentTemplate c in ComponentsList) c.Leave(context);
  }
  public void EarlyUpdate(Context context)
  {
    foreach (ComponentTemplate c in ComponentsList) c.EarlyUpdate(context);
  }
  public void Update(Context context)
  {
    foreach (ComponentTemplate c in ComponentsList) c.Update(context);
  }
  public void LateUpdate(Context context)
  {
    foreach (ComponentTemplate c in ComponentsList) c.LateUpdate(context);
  }
  public void PreDraw(Context context)
  {
    foreach (ComponentTemplate c in ComponentsList) c.PreDraw(context);
  }
  public void Draw(Context context)
  {
    foreach (ComponentTemplate c in ComponentsList) c.Draw(context);
  }
  
  public Item Clone() => (Item)MemberwiseClone();
}