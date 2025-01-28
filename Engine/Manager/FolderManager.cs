using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Node;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Manager;

public class FolderManager
{
  public List<IFolder<dynamic>> All { get; } = [];
  
  public Folder GetByParentFolder(Folder item) => (Folder)All.First(f => f.Parent!.Contains(item));
  public Folder GetByChildrenItem(Item item) => (Folder)All.First(f => f.Children.Contains(item));
  public Folder GetByChildrenFolder(Folder item) => (Folder)All.First(f => f.Children.Contains(item));
  public Folder GetByName(string name) => (Folder)All.First(f => f.Name == name);
  
  public void RegisterFromStruct()
  {
    foreach (var folder in typeof(Registration.Folders).GetFields().Select(f => f.GetValue(null)).OfType<Folder>().ToArray()) All.Add(folder);
  }
}