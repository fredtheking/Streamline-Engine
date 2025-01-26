using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Manager;

public class FolderManager
{
  public List<IFolder<dynamic>> All { get; } = [];
  
  public Node.Folder GetByParentFolder(Node.Folder item) => (Node.Folder)All.First(f => f.Parent!.Contains(item));
  public Node.Folder GetByChildrenItem(Item item) => (Node.Folder)All.First(f => f.Children!.Contains(item));
  public Node.Folder GetByChildrenFolder(Node.Folder item) => (Node.Folder)All.First(f => f.Children!.Contains(item));
  public Node.Folder GetByName(string name) => (Node.Folder)All.First(f => f.Name == name);
  
  public void RegisterFromStruct()
  {
    foreach (var folder in typeof(Registration.Folders).GetFields().Select(f => f.GetValue(null)).OfType<Node.Folder>().ToArray()) All.Add(folder);
  }
}