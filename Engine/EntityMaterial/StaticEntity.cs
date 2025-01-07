using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.EntityMaterial;

public class StaticEntity : UuidIdentifier, IScript
{
  public string Name { get; private set; }
  public string[] Scenes { get; private set; }
  public List<ComponentTemplate> Components { get; } = [];
  public List<IMaterial> Materials { get; } = [];

  public StaticEntity(GameContext context, string name, params Config.Scenes[] scenes)
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

  public void AddMaterial(IMaterial material) =>
    Materials.Add(material);

  public void Init(GameContext context)
  {
    Materials.ForEach(m => m.Init(context));
    Components.ForEach(c => c.Init(context));
  }

  public void Enter(GameContext context)
  {
    Materials.ForEach(m => m.Enter(context));
    Components.ForEach(c => c.Enter(context));
  }
  
  public void Leave(GameContext context)
  {
    Materials.ForEach(m => m.Leave(context));
    Components.ForEach(c => c.Leave(context));
  }
  public void Update(GameContext context)
  {
    Materials.ForEach(m => m.Update(context));
    Components.ForEach(c => c.Update(context));
  }
  public void Draw(GameContext context)
  {
    Materials.ForEach(m => m.Draw(context));
    Components.ForEach(c => c.Draw(context));
  }
}