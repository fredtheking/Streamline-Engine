using Raylib_cs;
using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

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

  public override void Init(MainContext context)
  {
    StaticEntity staticEntity = context.Managers.Entity.GetByComponent(this);
    Position = staticEntity.Component<PositionComponent>() ?? Error(new PositionComponent(), "Entity has no position component. Initialising default position.");
    Size = staticEntity.Component<SizeComponent>() ?? Error(new SizeComponent(), "Entity has no size component. Initialising default size.");
    Figure = staticEntity.Component<FigureComponent>() ?? Error(new FigureComponent(), "Entity has no figure component. Initialising default figure.");
  }

  public override void Draw(MainContext context)
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