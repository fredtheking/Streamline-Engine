using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.EntityMaterial;

public class StaticEntity : UuidIdentifier, IScript
{
  public string Name { get; private set; }
  public string[] Scenes { get; private set; }
  public List<ComponentTemplate> Components { get; } = [];
  public List<IMaterial> Materials { get; } = [];
  public List<(LateInitType, Action)> LateInitActions { get; } = [];

  public StaticEntity(string name, Config.Scenes[] scenes, ComponentTemplate[] components)
  {
    Name = name;
    Scenes = scenes.Select(s => s.ToString()).ToArray();
    //context.Managers.Entity.All.Add(Uuid, this);
    //scenes.ToList().ForEach(current => context.Managers.Scene.All.First(s => s.Name == current.ToString()).Entities.Add(Uuid, this));
    AddComponent(components.ToList());
  }
  
  public T? Component<T>() where T : ComponentTemplate =>
    Components.FirstOrDefault(c => c is T) as T;
  
  public T? Material<T>() where T : class, IMaterial =>
    Materials.FirstOrDefault(c => c is T) as T;

  public void AddComponent(params List<ComponentTemplate> component) =>
    component.ForEach(c => Components.Add(c));
  
  public void AddMaterial(IMaterial material)
  {
    if (!Materials.Contains(material)) Materials.Add(material);
  }

  public void AddLateInit(LateInitType type, Action action) =>
    LateInitActions.Add((type, action));

  public void LocalLateInit(dynamic component, bool pos = true, bool size = true)
  {
    if (pos) AddLateInit(LateInitType.Component, () => component.LocalPosition = new PositionComponent(0));
    if (size) AddLateInit(LateInitType.Component, () => component.LocalSize = new SizeComponent(0));
  }
  
  public void Init(MainContext context)
  {
    Materials.ForEach(m => m.Init(context));
    Components.ForEach(c => c.Init(context));
    LateInitActions.OrderBy(p => p.Item1).ToList().ForEach(p => p.Item2());
  }

  public void Enter(MainContext context)
  {
    Materials.ForEach(m => m.Enter(context));
    Components.ForEach(c => c.Enter(context));
  }
  
  public void Leave(MainContext context)
  {
    Materials.ForEach(m => m.Leave(context));
    Components.ForEach(c => c.Leave(context));
  }
  public void Update(MainContext context)
  {
    Materials.ForEach(m => m.Update(context));
    Components.ForEach(c => c.Update(context));
  }
  public void Draw(MainContext context)
  {
    Materials.ForEach(m => m.Draw(context));
    Components.ForEach(c => c.Draw(context));
  }
}