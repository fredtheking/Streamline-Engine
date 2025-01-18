using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Folder;
using StreamlineEngine.Engine.FolderItem;

namespace StreamlineEngine.Engine.Manager;

public class FolderManager
{
  public List<IFolder<dynamic>> All { get; } = [];
  
  public FolderNode GetByParentFolder(FolderNode folderItem) => (FolderNode)All.First(f => f.Parent!.Contains(folderItem));
  public FolderNode GetByChildrenItem(Item item) => (FolderNode)All.First(f => f.Children!.Contains(item));
  public FolderNode GetByChildrenFolder(FolderNode folderItem) => (FolderNode)All.First(f => f.Children!.Contains(folderItem));
  public FolderNode GetByName(string name) => (FolderNode)All.First(f => f.Name == name);
  
  public void RegisterFromStruct()
  {
    foreach (var folder in typeof(Registration.Folders).GetFields().Select(f => f.GetValue(null)).OfType<FolderNode>().ToArray()) All.Add(folder);
  }
}