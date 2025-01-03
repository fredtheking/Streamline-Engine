using Raylib_cs;
using StreamlineEngine.Engine.Pkg.ECS.EntityDir;
using StreamlineEngine.Engine.Pkg.Etc;
using Color = Raylib_cs.Color;
using Rectangle = Raylib_cs.Rectangle;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class FillComponent : ComponentGroup
{
  public PositionComponent Position { get; set; }
  public SizeComponent Size { get; set; }
  public FigureComponent Figure { get; set; }
  public Color Color { get; set; }

  public FillComponent(Color color) { Color = color; }
  public FillComponent() { Color = Color.Blank; }

  public override void Init(GameContext context)
  {
    Entity entity = context.Entities.Values.First(e => e.Components.Contains(this));
    Position = entity.Component<PositionComponent>() ?? Warning(new PositionComponent(), "WARN 'FillComponent': Filler has no position component. Initialising default position.");
    Size = entity.Component<SizeComponent>() ?? Warning(new SizeComponent(), "WARN 'FillComponent': Filler has no size component. Initialising default size.");
    Figure = entity.Component<FigureComponent>() ?? Warning(new FigureComponent(), "WARN 'FillComponent': Filler has no figure component. Initialising default rectangle figure.");
  }

  public override void Draw(GameContext context)
  {
    switch (Figure.Type)
    {
      case FigureType.Rectangle:
        Raylib.DrawRectangleV(Position.Vec2, Size.Vec2, Color);
        break;
      case FigureType.Rounded:
        Raylib.DrawRectangleRounded(new Rectangle(Position.Vec2, Size.Vec2), Figure.Roundness, Config.RoundedSegments, Color);
        break;
      case FigureType.Circle:
        Raylib.DrawEllipse((int)(Position.X + Size.Width / 2), (int)(Position.Y + Size.Height / 2), Size.Width, Size.Height, Color);
        break;
    }
  }
}