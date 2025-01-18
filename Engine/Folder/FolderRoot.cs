using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.FolderItem;
using StreamlineEngine.Engine.Manager;

namespace StreamlineEngine.Engine.Folder;

public class FolderRoot : UuidIdentifier, IFolder<FolderNode>
{
  private List<dynamic>? _children;
  public string Name { get; set; }
  public bool Active { get; set; }
  public List<object>? Parent { get; set; } = null;
  public List<FolderNode> Children { get; set; }
  public FolderNode[] SortedChildren { get; set; } = [];

  public void Init(Context context)
  {
    foreach (FolderNode node in Children)
      node.Init(context);
  }
  public void Enter(Context context)
  {
    foreach (FolderNode node in SortedChildren)
      node.Enter(context);
  }
  public void Leave(Context context)
  {
    foreach (FolderNode node in SortedChildren)
      node.Leave(context);
  }
  public void Update(Context context)
  {
    foreach (FolderNode node in SortedChildren)
      node.Update(context);
  }

  public void Draw(Context context)
  {
    foreach (FolderNode node in SortedChildren)
      node.Draw(context);
  }

  public void Change(Context context, FolderNode folder)
  {
    if (folder.Type != FolderNodeType.Scene)
    {
      Error("Expected a FolderNode of type Scene, got: " + folder.Type);
      return;
    }
    context.Managers.Debug.PrintSeparator(ConsoleColor.Blue, $"Leaving from '{context.Root.Children.FirstOrDefault(c => c is { Active: true, Type: FolderNodeType.Scene })?.Name ?? "INITIALISATION"}' scene...");
    
    foreach (FolderNode node in Children.Where(c => c.Type == FolderNodeType.Scene))
      node.Active = false;
    folder.Active = true;
    
    Looper.Leave(context);
    SortedChildren = Children.Where(c => c is { Type: FolderNodeType.Scene, Active: true }).ToArray();
    Looper.Enter(context);
    SortedChildren = SortedChildren.Concat(Children.Where(c => c.Type != FolderNodeType.Scene)).ToArray();
      
    context.Managers.Debug.PrintSeparator(ConsoleColor.Green, $"Successfully entered '{context.Root.Children.First(c => c is { Active: true, Type: FolderNodeType.Scene }).Name}' scene!");
  }

  public void Next(Context context)
  {
    if (Children.Count(c => c.Type == FolderNodeType.Scene) == 1)
    {
      context.Managers.Debug.PrintSeparator(ConsoleColor.Red, "Only one scene, can't go forward!");
      return;
    }
    int index = (Children.IndexOf(Children.FirstOrDefault(c => c is { Type: FolderNodeType.Scene, Active: true })!) + 1) % Children.Count;
    Change(context, Children[index]);
  }

  public void Previous(Context context)
  {
    if (Children.Count(c => c.Type == FolderNodeType.Scene) == 1)
    {
      context.Managers.Debug.PrintSeparator(ConsoleColor.Red, "Only one scene, can't go back!");
      return;
    }
    int index = (Children.LastIndexOf(Children.LastOrDefault(c => c is { Type: FolderNodeType.Scene, Active: true })!) - 1 + Children.Count) % Children.Count;
    Change(context, Children[index]);
  }

  public FolderRoot(params FolderNode[] children) {
    Name = "Root";
    Children = children.ToList();
  }
}