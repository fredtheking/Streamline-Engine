using Raylib_cs;
using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class BorderComponent : ComponentTemplate
{
  public PositionComponent Position { get; set; }
  public SizeComponent Size { get; set; }
  public FigureComponent Figure { get; set; }
  public float Thickness { get; set; }
  public Color Color { get; set; }

  public BorderComponent()
  {
    Thickness = 1f;
    Color = Color.LightGray;
  }
  public BorderComponent(Color color)
  {
    Thickness = 1f;
    Color = color;
  }
  public BorderComponent(float thickness)
  {
    Thickness = thickness;
    Color = Color.LightGray;
  }
  public BorderComponent(float thickness, Color color)
  {
    Thickness = thickness;
    Color = color;
  }

  public override void Init(GameContext context)
  {
    FillComponent? objectComponent = context.Managers.Entity.GetEntityByComponent(this).Component<FillComponent>();
    if (objectComponent is not null)
    {
      Position = objectComponent.Position;
      Size = objectComponent.Size;
      Figure = objectComponent.Figure;
      if (Figure.Type == FigureType.Circle)
        Warning("Circle border can be only 1px. Thickness parameter can/will be ignored.");
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
        Raylib.DrawRectangleRoundedLinesEx(new Rectangle(Position.Vec2, Size.Vec2), Figure.Roundness, Defaults.RoundedSegments, Thickness, Color);
        break;
      case FigureType.Circle:
        Raylib.DrawEllipseLines((int)(Position.X + Size.Width / 2), (int)(Position.Y + Size.Height / 2), Size.Width, Size.Height, Color);
        break;
    }
  }
}