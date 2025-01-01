using StreamlineEngine.Engine.Pkg.ECS.Component;
using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.ECS.Entity;

public class Entity
{
  public string Name { get; private set; }
  public string Uuid { get; private set; }
  public ComponentGroup[] Components { get; private set; }

  public Entity(string name, params ComponentGroup[] components)
  {
    Name = name;
    Uuid = Guid.NewGuid().ToString("D");
    Components = components;
  }
  
  public ComponentGroup[] GetComponentsOfType<T>() where T : ComponentGroup =>
    Components.Where(c => c is T).ToArray();
}