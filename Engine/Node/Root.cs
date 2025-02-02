using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Node;

#if !RESOURCES
public class Root : UuidIdentifier, IFolder<Folder>
{
  private List<dynamic>? _children;
  public string Name { get; set; }
  public bool Active { get; set; }
  public List<object>? Parent { get; set; } = null;
  public List<Folder> Children { get; set; }
  public Folder CurrentScene { get; private set; }
  public Folder[] ActiveChildren { get; private set; } = [];
  public Folder[] Scenes { get; private init; }

  public void Init(Context context)
  {
    foreach (Folder node in Children)
    {
      node.Init(context);
      node.Parent!.Add(this);
    }
  }
  public void Enter(Context context)
  {
    foreach (Folder node in ActiveChildren)
      node.Enter(context);
  }
  public void Leave(Context context)
  {
    foreach (Folder node in ActiveChildren.Intersect(Scenes))
      node.Leave(context);
  }
  public void Update(Context context)
  {
    foreach (Folder node in ActiveChildren)
      node.Update(context);
  }

  public void Draw(Context context)
  {
    foreach (Folder node in ActiveChildren)
      node.Draw(context);
  }

  public void Change(Context context, Folder folder)
  {
    if (folder.Type != FolderNodeType.Scene)
    {
      Critical(context, "Expected a FolderNode of type Scene, got: " + folder.Type);
      return;
    }

    string? oldSceneName = null;
    if (Children.Count(c => c is {Type: FolderNodeType.Scene, Active: true}) == 1)
      oldSceneName = context.Root.Children.FirstOrDefault(c => c is { Active: true, Type: FolderNodeType.Scene })?.Name;
    if (oldSceneName is not null) context.Managers.Debug.Separator(ConsoleColor.Blue, $"Leaving from '{oldSceneName}' scene...", '~');
    
    foreach (Folder node in Scenes)
      node.Active = false;
    CurrentScene = folder;
    CurrentScene.Active = true;
    
    Looper.Leave(context);
    ActiveChildren = Children.Where(c => c is { Type: FolderNodeType.Scene, Active: true }).Concat(Children.Where(c => c.Type != FolderNodeType.Scene)).ToArray();
    Looper.Enter(context);
      
    if (oldSceneName is not null) context.Managers.Debug.Separator(ConsoleColor.Green, $"Successfully entered '{context.Root.Children.First(c => c is { Active: true, Type: FolderNodeType.Scene }).Name}' scene!", '~');
  }

  public void Next(Context context)
  {
    if (Children.Count(c => c.Type == FolderNodeType.Scene) == 1)
    {
      Error(context, "Only one scene, can't go forward!");
      return;
    }
    int index = (Scenes.ToList().FindIndex(c => c == CurrentScene) + 1) % Scenes.Length;
    Change(context, Children[index]);
  }

  public void Previous(Context context)
  {
    if (Children.Count(c => c.Type == FolderNodeType.Scene) == 1)
    {
      Error(context, "Only one scene, can't go back!");
      return;
    }
    int index = (Scenes.ToList().FindIndex(c => c == CurrentScene) - 1 + Scenes.Length) % Scenes.Length;
    Change(context, Children[index]);
  }

  public Root(params Folder[] children) {
    Name = "Root";
    Children = children.ToList();
    Scenes = Children.Where(c => c.Type == FolderNodeType.Scene).ToArray();
  }
}
#endif