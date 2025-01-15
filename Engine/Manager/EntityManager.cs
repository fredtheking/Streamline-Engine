using System.Reflection;
using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Pkg.Manager;

public class EntityManager
{
  public Dictionary<string, StaticEntity> All { get; } = [];
  
  public StaticEntity GetByComponent(ComponentTemplate component) => All.Values.First(e => e.Components.Contains(component));
  public StaticEntity GetByName(string name) => All.Values.First(e => e.Name == name);
  public StaticEntity GetByUuid(string uuid) => All.Values.First(e => e.Uuid == uuid);

  public void RegisterFromStruct(MainContext context)
  {
    foreach (var entity in typeof(Registration.Entities).GetFields().Select(f => f.GetValue(null)).OfType<StaticEntity>().ToArray())
    {
      context.Managers.Entity.All.Add(entity.Uuid, entity);
      entity.Scenes.ToList().ForEach(current => context.Managers.Scene.All.First(s => s.Name == current.ToString()).Entities.Add(entity.Uuid, entity));
    }
  }
}