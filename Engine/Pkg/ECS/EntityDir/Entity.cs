using StreamlineEngine.Engine.Pkg.ECS.ComponentDir;
using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Etc.Interfaces;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Pkg.ECS.EntityDir;

public class Entity : UuidIdentifier, IScript
{
  public string Name { get; private set; }
  public string[] Scenes { get; private set; }
  public List<ComponentTemplate> Components { get; } = [];

  public Entity(GameContext context, string name, params Config.Scenes[] scenes)
  {
    Name = name;
    Scenes = scenes.Select(s => s.ToString()).ToArray();
    context.Managers.Entity.All.Add(Uuid, this);
    foreach (string scene in Scenes) 
      context.Managers.Scene.All.First(s => s.Name == scene).Entities.Add(Uuid, this);
  }
  
  public T? Component<T>() where T : ComponentTemplate =>
    Components.FirstOrDefault(c => c is T) as T;

  public void AddComponent(ComponentTemplate component) =>
    Components.Add(component);

  public void Init(GameContext context) =>
    Components.ForEach(c => c.Init(context));

  public void Enter(GameContext context) =>
    Components.ForEach(c => c.Enter(context));
  
  public void Leave(GameContext context) =>
    Components.ForEach(c => c.Leave(context));

  public void Update(GameContext context) =>
    Components.ForEach(c => c.Update(context));

  public void Draw(GameContext context) =>
    Components.ForEach(c => c.Draw(context));
}