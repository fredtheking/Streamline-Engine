using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Component;

public class BorderComponent : ComponentTemplate
{
  public PositionComponent LocalPosition { get; set; }
  public PositionComponent Position { get; set; }
  public SizeComponent LocalSize { get; set; }
  public SizeComponent Size { get; set; }
  public FigureComponent Figure { get; set; }
  public float Thickness { get; set; }
  public Color Color { get; set; }

  public BorderComponent()
  {
    Thickness = 1f;
    Color = Color.LightGray;
    DebugBorderColor = Color.Blank;
  }
  public BorderComponent(Color color)
  {
    Thickness = 1f;
    Color = color;
    DebugBorderColor = Color.Blank;
  }
  public BorderComponent(float thickness)
  {
    Thickness = thickness;
    Color = Color.LightGray;
    DebugBorderColor = Color.Blank;
  }
  public BorderComponent(float thickness, Color color)
  {
    Thickness = thickness;
    Color = color;
    DebugBorderColor = Color.Blank;
  }

  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      Position = Parent.ComponentTry<PositionComponent>() ?? Warning(context, new PositionComponent(), "Item has no position component. Initialising default position.");
      Size = Parent.ComponentTry<SizeComponent>() ?? Warning(context, new SizeComponent(), "Item has no size component. Initialising default size.");
      Figure = Parent.ComponentTry<FigureComponent>() ?? Warning(context, new FigureComponent(), "Item has no figure component. Initialising default figure.");
    
      Parent.LocalPosSizeToLateInit(this);
    });
  }

  public override void Draw(Context context)
  {
    switch (Figure.Type)
    {
      case FigureType.Rectangle:
        Raylib.DrawRectangleLinesEx(new Rectangle(Position.Vec2 + LocalPosition.Vec2, Size.Vec2 + LocalSize.Vec2), Thickness, Color);
        break;
      case FigureType.Rounded:
        Raylib.DrawRectangleRoundedLinesEx(new Rectangle(Position.Vec2 + LocalPosition.Vec2, Size.Vec2 + LocalSize.Vec2), Figure.Roundness, Defaults.RoundedSegments, Thickness, Color);
        break;
      case FigureType.Circle:
        Raylib.DrawEllipseLines((int)(Position.X + LocalPosition.X + Size.Width / 2 + LocalSize.Width / 2), (int)(Position.Y + LocalPosition.Y + Size.Height / 2 + LocalSize.Height / 2), Size.Width + LocalSize.Width, Size.Height + LocalSize.Height, Color);
        break;
    }
  }
}