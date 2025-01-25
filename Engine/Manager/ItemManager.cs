using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Manager;

public class ItemManager
{
  public List<Item.Item> All { get; } = [];
  
  public Item.Item GetByComponent(ComponentTemplate component) => All.First(i => i.Components.Contains(component));
  public Item.Item GetByName(string name) => All.First(i => i.Name == name);
  public Item.Item GetByUuid(string uuid) => All.First(i => i.Uuid == uuid);

  public void RegisterFromStruct()
  {
    foreach (var item in typeof(Registration.Items).GetFields().Select(f => f.GetValue(null)).OfType<Item.Item>().ToArray()) All.Add(item);
  }
}