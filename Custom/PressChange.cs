using Raylib_cs;
using StreamlineEngine.Engine;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Custom;

public class PressChange : IScript
{
  public void Init(MainContext context) { }

  public void Enter(MainContext context) { }

  public void Leave(MainContext context) { }

  public void Update(MainContext context)
  {
    if (Registration.Items.Item.Component<MouseHitboxComponent>()!.Click[0])
      Registration.Items.Item.Component<MouseHitboxComponent>()!.Color = new Color(0, 0, 255, 75);
  }

  public void Draw(MainContext context) { }
}