using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Pkg.ECS.EntityDir;
using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class ObjectComponent : ComponentGroup
{
  public PositionComponent Position { get; set; }
  public SizeComponent Size { get; set; }
  public FigureComponent Figure { get; set; }
  public Color Color { get; set; }

  public ObjectComponent(PositionComponent posComponent, SizeComponent sizeComponent, Color color)
  {
    Position = posComponent; 
    Size = sizeComponent;
    Color = color;
  }

  public override void Init(GameContext context)
  {
    Figure = context.Entities.Values.First(e => e.Components.Contains(this)).Component<FigureComponent>() ?? new FigureComponent(FigureType.Rectangle);
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
        Raylib.DrawEllipse(Position.X+Size.Width/2, Position.Y+Size.Height/2, Size.Width, Size.Height, Color);
        break;
    }
  }
}