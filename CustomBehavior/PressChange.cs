using Raylib_cs;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.CustomBehavior;

public class PressChange : CustomItemBehaviorTemplate
{
  PositionComponent Position;
  MouseHitboxComponent Hitbox;
  BorderComponent Border;
  FillComponent Fill;
  
  Color[] colors;
  Color[] borderColors;
  Color[] fillColors;
  bool selected;

  public override void Init(Context context)
  {
    colors = [Defaults.DebugHitboxColor, new(0, 0, 255, 75)];
    borderColors = [Color.Red, Color.Blue];
    fillColors = [new Color(255, 200, 200), new Color(200, 200, 255)];

    Position = Parent.Component<PositionComponent>();
    Hitbox = Parent.Component<MouseHitboxComponent>();
    Border = Parent.Component<BorderComponent>();
    Fill = Parent.Component<FillComponent>();
  }

  public override void Enter(Context context)
  {
    
  }

  public override void Update(Context context)
  {
    Position.Set((float)Math.Sin(Raylib.GetTime())*200 + 500, (float)Math.Cos(Raylib.GetTime())*100 + 200);
    
    selected = Hitbox.Drag[(int)MouseButton.Left];
    Hitbox.Color = colors[selected ? 1 : 0];
    Border.Color = borderColors[selected ? 1 : 0];
    Fill.Color = fillColors[selected ? 1 : 0];
  }
}