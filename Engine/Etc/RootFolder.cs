using StreamlineEngine.Engine.FolderItem;

namespace StreamlineEngine.Engine.Etc;

public class RootFolder : Folder
{
  public RootFolder(string name, params dynamic[]? children) : base(name, children) { }
  public bool Changed { get; set; }

  public void Change(MainContext context, Folder folder)
  {
    Changed = true;
    Children?.Clear();
    Children?.Add(folder);
    foreach (var c in Children ?? Enumerable.Empty<dynamic>()) c.Enter(context);
  }

  public override void Init(MainContext context) {
    Change(context, Config.StartScene);
    base.Init(context);
  }
}