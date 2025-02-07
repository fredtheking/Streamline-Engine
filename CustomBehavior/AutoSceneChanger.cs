using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.CustomBehavior;

public class AutoSceneChanger : CustomItemBehaviorTemplate
{
  public override void Update(Context context)
  {
    if (context.Managers.Debug.TurnedOn) return;
    if (context.Root.CurrentScene == Registration.Folders.FirstScene)
      context.Root.Change(context, Registration.Folders.SecondScene);
    else if (context.Root.CurrentScene == Registration.Folders.SecondScene)
      context.Root.Change(context, Registration.Folders.FirstScene);
  }
}