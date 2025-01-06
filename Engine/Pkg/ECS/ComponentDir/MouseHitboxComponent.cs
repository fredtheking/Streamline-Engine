using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Pkg.ECS.EntityDir;
using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class MouseHitboxComponent : ComponentTemplate
{
  public PositionComponent Position { get; set; }
  public SizeComponent Size { get; set; }
  public FigureComponent Figure { get; set; }
  public Color Color { get; set; }
  public bool[] Hover { get; private set; } = [false, false, false];
  public bool[] Click { get; private set; } = [false, false, false];
  public bool[] Press { get; private set; } = [false, false, false];
  public bool[] Release { get; private set; } = [false, false, false];
  public bool[] Hold { get; private set; } = [false, false, false];
  public bool[] Drag { get; private set; } = [false, false, false];
  
  public MouseHitboxComponent() { Color = Defaults.DebugHitboxColor; }
  public MouseHitboxComponent(Color debugColor) { Color = debugColor; }

  public override void Init(GameContext context)
  {
    Entity entity = context.Managers.Entity.GetEntityByComponent(this);
    Position = entity.Component<PositionComponent>() ?? Warning(new PositionComponent(), "Entity has no position component. Initialising default position.");
    Size = entity.Component<SizeComponent>() ?? Warning(new SizeComponent(), "Entity has no position component. Initialising default position.");
    Figure = entity.Component<FigureComponent>() ?? Warning(new FigureComponent(), "Entity has no position component. Initialising default position.");
  }

  private bool DecideHover()
  {
    switch (Figure.Type)
    {
      case FigureType.Rectangle:
        return Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(Position.Vec2, Size.Vec2));
      case FigureType.Rounded:
        return Warning(Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(Position.Vec2, Size.Vec2)), "Hovering on rounded figure works as same as rectangle. Be careful!", true);
      case FigureType.Circle:
        if (Math.Abs(Size.Width - Size.Height) > 0)
          Warning("Entity's size is not square. Hovering on circle will not work as expected. Be careful!", true);
        return Raylib.CheckCollisionPointCircle(Raylib.GetMousePosition(), new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height / 2), Size.Width / 2);
    }
    return false;
  }

  public override void Update(GameContext context)
  {
    for (int i = 0; i < Hover.Length; i++)
    {
      Hover[i] = DecideHover();
      Click[i] = Raylib.IsMouseButtonPressed((MouseButton)i);
      Press[i] = Click[i] && Hover[i];
      Release[i] = Raylib.IsMouseButtonReleased((MouseButton)i);
      Hold[i] = Raylib.IsMouseButtonPressed((MouseButton)i);
      if (!Drag[i] & Click[i] & Hover[i]) 
        Drag[i] = true;
      if (Drag[i] & !Hold[i]) 
        Drag[i] = false;
    }
  }

  public override void Draw(GameContext context)
  {
    if (!context.Managers.Debug.TurnedOn) return;

    switch (Figure.Type)
    {
      case FigureType.Rectangle or FigureType.Rounded:
        Raylib.DrawRectangleV(Position.Vec2, Size.Vec2, Color);
        break;
      case FigureType.Circle:
        Raylib.DrawCircle((int)(Position.X + Size.Width / 2), (int)(Position.Y + Size.Height / 2), Math.Max(Size.Width, Size.Height), Color);
        break;
    }
  }
}