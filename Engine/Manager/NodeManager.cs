using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Node;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Manager;

public class NodeManager
{
  public List<Folder> All { get; } = [];
  
  public Folder GetByParentFolder(Folder folder) => All.First(f => f.Parent!.Contains(folder));
  public Folder GetByChildrenItem(Item item) => All.First(f => f.Children!.Contains(item));
  public Folder GetByChildrenFolder(Folder folder) => All.First(f => f.Children!.Contains(folder));
  public Folder GetByName(string name) => All.First(f => f.Name == name);
  public Folder GetByUuid(string uuid) => All.First(i => i.Uuid == uuid);
  
  #if !RESOURCES
  public void RegisterFromStruct()
  {
    foreach (var folder in typeof(Registration.Folders).GetFields().Select(f => f.GetValue(null)).OfType<Folder>().ToArray()) All.Add(folder);
  }
  #endif
}