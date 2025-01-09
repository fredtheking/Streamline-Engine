using Raylib_cs;
using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Pkg.Etc.Templates;
using Color = Raylib_cs.Color;
using Rectangle = Raylib_cs.Rectangle;

namespace StreamlineEngine.Engine.Component;

public class FillComponent : ComponentTemplate
{
  public PositionComponent Position { get; set; }
  public SizeComponent Size { get; set; }
  public FigureComponent Figure { get; set; }
  public Color Color { get; set; }

  public FillComponent(Color color) { Color = color; }
  public FillComponent() { Color = Color.White; }

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
        Raylib.DrawRectangleV(Position.Vec2, Size.Vec2, Color);
        break;
      case FigureType.Rounded:
        Raylib.DrawRectangleRounded(new Rectangle(Position.Vec2, Size.Vec2), Figure.Roundness, Defaults.RoundedSegments, Color);
        break;
      case FigureType.Circle:
        Raylib.DrawEllipse((int)(Position.X + Size.Width / 2), (int)(Position.Y + Size.Height / 2), Size.Width, Size.Height, Color);
        break;
    }
  }
}