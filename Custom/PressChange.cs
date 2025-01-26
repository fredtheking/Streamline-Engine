using Raylib_cs;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Custom;

public class PressChange : CustomItemBehaviorTemplate
{
  Color[] colors;
  Color[] borderColors;
  Color[] fillColors;
  bool selected;

  public override void Init(Context context)
  {
    colors = [Defaults.DebugHitboxColor, new(0, 0, 255, 75)];
    borderColors = [Color.Red, Color.Blue];
    fillColors = [new Color(255, 200, 200), new Color(200, 200, 255)];
  }

  public override void Update(Context context)
  {
    #if !RESOURCES
    selected = Parent.Component<MouseHitboxComponent>()!.Hold[(int)MouseButton.Left];
    Parent.Component<MouseHitboxComponent>()!.Color = colors[selected ? 1 : 0];
    Parent.Component<BorderComponent>()!.Color = borderColors[selected ? 1 : 0];
    Parent.Component<FillComponent>()!.Color = fillColors[selected ? 1 : 0];
    #endif
  }
}