using Raylib_cs;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.CustomBehavior;

#if !RESOURCES
public class AutoSceneChanger : CustomItemBehaviorTemplate
{
  public override void Update(Context context)
  {
    if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.A))
      Registration.Items.Item2.Component<AnimationComponent>().Index -= 1;
    if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.D))
      Registration.Items.Item2.Component<AnimationComponent>().Index += 1;
  }
}
#endif