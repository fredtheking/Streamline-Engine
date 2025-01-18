using Raylib_cs;
using StreamlineEngine.Engine;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Custom;

public class PressChange : IScript
{
  public void Init(Context context) { }

  public void Enter(Context context) { }

  public void Leave(Context context) { }

  public void Update(Context context)
  {
    if (Registration.Items.Item.Component<MouseHitboxComponent>()!.Click[0])
      Registration.Items.Item.Component<MouseHitboxComponent>()!.Color = new Color(0, 0, 255, 75);
  }

  public void Draw(Context context) { }
}