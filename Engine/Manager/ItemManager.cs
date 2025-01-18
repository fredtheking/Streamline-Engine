using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.FolderItem;

namespace StreamlineEngine.Engine.Manager;

public class ItemManager
{
  public List<Item> All { get; } = [];
  
  public Item GetByComponent(ComponentTemplate component) => All.First(i => i.Components.Contains(component));
  public Item GetByName(string name) => All.First(i => i.Name == name);
  public Item GetByUuid(string uuid) => All.First(i => i.Uuid == uuid);

  public void RegisterFromStruct()
  {
    foreach (var item in typeof(Registration.Items).GetFields().Select(f => f.GetValue(null)).OfType<Item>().ToArray()) All.Add(item);
  }
}