using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.FolderItem;
using StreamlineEngine.Engine.Manager;

namespace StreamlineEngine.Engine.Folder;

public class Root : UuidIdentifier, IFolder<FolderNode>
{
  private List<dynamic>? _children;
  public string Name { get; set; }
  public bool Active { get; set; }
  public List<object>? Parent { get; set; } = null;
  public List<FolderNode> Children { get; set; }
  public FolderNode CurrentScene { get; private set; }
  public FolderNode[] ActiveChildren { get; private set; } = [];
  public FolderNode[] Scenes { get; private set; } = [];

  public void Init(Context context)
  {
    foreach (FolderNode node in Children)
      node.Init(context);
  }
  public void Enter(Context context)
  {
    foreach (FolderNode node in ActiveChildren)
      node.Enter(context);
  }
  public void Leave(Context context)
  {
    foreach (FolderNode node in ActiveChildren)
      node.Leave(context);
  }
  public void Update(Context context)
  {
    foreach (FolderNode node in ActiveChildren)
      node.Update(context);
  }

  public void Draw(Context context)
  {
    foreach (FolderNode node in ActiveChildren)
      node.Draw(context);
  }

  public void Change(Context context, FolderNode folder)
  {
    if (folder.Type != FolderNodeType.Scene)
    {
      Error("Expected a FolderNode of type Scene, got: " + folder.Type);
      return;
    }

    string? oldSceneName = context.Root.Children.FirstOrDefault(c => c is { Active: true, Type: FolderNodeType.Scene })?.Name;
    if (oldSceneName is not null) context.Managers.Debug.PrintSeparator(ConsoleColor.Blue, $"Leaving from '{oldSceneName}' scene...");
    
    foreach (FolderNode node in Children.Where(c => c.Type == FolderNodeType.Scene))
      node.Active = false;
    CurrentScene = folder;
    CurrentScene.Active = true;
    
    Scenes = Children.Where(c => c.Type == FolderNodeType.Scene).ToArray();
    ActiveChildren = Children.Where(c => c is { Type: FolderNodeType.Scene, Active: true }).ToArray();
    Looper.Leave(context);
    ActiveChildren = ActiveChildren.Concat(Children.Where(c => c.Type != FolderNodeType.Scene)).ToArray();
    Looper.Enter(context);
      
    if (oldSceneName is not null) context.Managers.Debug.PrintSeparator(ConsoleColor.Green, $"Successfully entered '{context.Root.Children.First(c => c is { Active: true, Type: FolderNodeType.Scene }).Name}' scene!");
  }

  public void Next(Context context)
  {
    if (Children.Count(c => c.Type == FolderNodeType.Scene) == 1)
    {
      Error("Only one scene, can't go forward!");
      return;
    }
    FolderNode[] Scenes = Children.Where(c => c is {Type: FolderNodeType.Scene}).ToArray(); 
    int index = (Scenes.ToList().FindIndex(c => c == CurrentScene) + 1) % Scenes.Length;
    Change(context, Children[index]);
  }

  public void Previous(Context context)
  {
    if (Children.Count(c => c.Type == FolderNodeType.Scene) == 1)
    {
      Error("Only one scene, can't go back!");
      return;
    }

    FolderNode[] Scenes = Children.Where(c => c is {Type: FolderNodeType.Scene}).ToArray(); 
    int index = (Scenes.ToList().FindIndex(c => c == CurrentScene) - 1 + Scenes.Length) % Scenes.Length;
    Change(context, Children[index]);
  }

  public Root(params FolderNode[] children) {
    Name = "Root";
    Children = children.ToList();
  }
}