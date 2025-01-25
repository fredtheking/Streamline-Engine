using Raylib_cs;
using StreamlineEngine.Engine;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Item;

namespace StreamlineEngine.Custom;

public class PressChange : IScript
{
  private Color[] colors;
  private bool selected;

  public void Init(Context context)
  {
    colors = [Defaults.DebugHitboxColor, new(0, 0, 255, 75)];
  }

  public void Enter(Context context) { }

  public void Leave(Context context) { }

  public void Update(Context context)
  {
    #if !RESOURCES
    if (Registration.Items.Item.Component<MouseHitboxComponent>()!.Press[(int)MouseButton.Left])
      selected = !selected;
    Registration.Items.Item.Component<MouseHitboxComponent>()!.Color = colors[selected ? 1 : 0];
    #endif
  }

  public void Draw(Context context) { }
}