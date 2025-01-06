using StreamlineEngine.Engine.Pkg.ECS.ComponentDir;
using StreamlineEngine.Engine.Pkg.ECS.EntityDir;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Pkg.Manager;

public class EntityManager
{
  public Dictionary<string, Entity> All { get; set; } = [];
  
  public Entity GetEntityByComponent(ComponentTemplate component) => All.Values.First(e => e.Components.Contains(component));
}