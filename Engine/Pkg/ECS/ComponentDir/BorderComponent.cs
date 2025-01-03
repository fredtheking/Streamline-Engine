using Raylib_cs;
using StreamlineEngine.Engine.Pkg.Etc;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class BorderComponent : ComponentGroup
{
  public PositionComponent Position { get; set; }
  public SizeComponent Size { get; set; }
  public FigureComponent Figure { get; set; }
  public float Thickness { get; set; }
  public Color Color { get; set; }

  public BorderComponent(Color color)
  {
    Thickness = 1;
    Color = color;
  }
  public BorderComponent(float thickness, Color color)
  {
    Thickness = thickness;
    Color = color;
  }

  public override void Init(GameContext context)
  {
    FillComponent? objectComponent = context.Entities.Values.First(e => e.Components.Contains(this)).Component<FillComponent>();
    if (objectComponent is not null)
    {
      Position = objectComponent.Position;
      Size = objectComponent.Size;
      Figure = objectComponent.Figure;
      if (Figure.Type == FigureType.Circle)
        Console.WriteLine("WARN 'BorderComponent': Circle border can be only 1px. Thickness parameter can/will be ignored.");
    }
    else
    {
      Position = new PositionComponent(0);
      Size = new SizeComponent(0);
      Figure = new FigureComponent();
    }
  }

  public override void Draw(GameContext context)
  {
    switch (Figure.Type)
    {
      case FigureType.Rectangle:
        Raylib.DrawRectangleLinesEx(new Rectangle(Position.Vec2, Size.Vec2), Thickness, Color);
        break;
      case FigureType.Rounded:
        Raylib.DrawRectangleRoundedLinesEx(new Rectangle(Position.Vec2, Size.Vec2), Figure.Roundness, Config.RoundedSegments, Thickness, Color);
        break;
      case FigureType.Circle:
        Raylib.DrawEllipseLines((int)(Position.X + Size.Width / 2), (int)(Position.Y + Size.Height / 2), Size.Width, Size.Height, Color);
        break;
    }
  }
}