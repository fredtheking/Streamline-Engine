using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Manager;

public class ItemManager
{
  public List<Item> All { get; } = [];
  
  public Item GetByComponent(ComponentTemplate component) => All.First(i => i.ComponentsList.Contains(component));
  public Item GetByName(string name) => All.First(i => i.Name == name);
  public Item GetByUuid(string uuid) => All.First(i => i.Uuid == uuid);
  
  #if !RESOURCES
  public void RegisterFromStruct()
  {
    foreach (var item in typeof(Registration.Items).GetFields().Select(f => f.GetValue(null)).OfType<Item>().ToArray()) All.Add(item);
  }
  #endif
}