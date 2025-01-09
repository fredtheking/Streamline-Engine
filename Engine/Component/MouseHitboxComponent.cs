using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class MouseHitboxComponent : ComponentTemplate
{
  public PositionComponent Position { get; set; }
  public SizeComponent Size { get; set; }
  public FigureComponent Figure { get; set; }
  public BorderComponent Border { get; set; }
  public Color Color { get; set; }
  public bool[] Hover { get; private set; } = [false, false, false];
  public bool[] Click { get; private set; } = [false, false, false];
  public bool[] Press { get; private set; } = [false, false, false];
  public bool[] Release { get; private set; } = [false, false, false];
  public bool[] Hold { get; private set; } = [false, false, false];
  public bool[] Drag { get; private set; } = [false, false, false];
  
  public MouseHitboxComponent() { Color = Defaults.DebugHitboxColor; }
  public MouseHitboxComponent(Color debugColor) { Color = debugColor; }

  public override void Init(MainContext context)
  {
    StaticEntity staticEntity = context.Managers.Entity.GetByComponent(this);
    Position = staticEntity.Component<PositionComponent>() ?? Error(new PositionComponent(), "Entity has no position component. Initialising default position.");
    Size = staticEntity.Component<SizeComponent>() ?? Error(new SizeComponent(), "Entity has no size component. Initialising default size.");
    Figure = staticEntity.Component<FigureComponent>() ?? Error(new FigureComponent(), "Entity has no figure component. Initialising default figure.");
    Border = Figure.Type == FigureType.Rounded ? staticEntity.Component<BorderComponent>() ?? new BorderComponent(0, Color.Blank) : new BorderComponent(0, Color.Blank);
  }

  private bool DecideHover()
  {
    switch (Figure.Type)
    {
      case FigureType.Rectangle:
        return Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(Position.Vec2, Size.Vec2));
      case FigureType.Rounded:
        return Warning(Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(Position.Vec2 - new Vector2(Border.Thickness), Size.Vec2 + new Vector2(Border.Thickness * 2))), "Hitbox on rounded figure works as same as rectangle ones. Be careful with corners!", true);
      case FigureType.Circle:
        if (Math.Abs(Size.Width - Size.Height) > 0)
          Error("Entity's size is not square. Hitbox is now stretched to circle. Be careful!", true);
        return Raylib.CheckCollisionPointCircle(Raylib.GetMousePosition(), new Vector2(Position.X + Size.Width / 2, Position.Y + Size.Height / 2), Size.Width / 2);
    }
    return false;
  }

  public override void Update(MainContext context)
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

  public override void Draw(MainContext context)
  {
    if (!context.Managers.Debug.TurnedOn) return;

    switch (Figure.Type)
    {
      case FigureType.Rectangle or FigureType.Rounded:
        Raylib.DrawRectangleV(Position.Vec2 - new Vector2(Border.Thickness), Size.Vec2 + new Vector2(Border.Thickness * 2), Color);
        break;
      case FigureType.Circle:
        Raylib.DrawCircle((int)(Position.X + Size.Width / 2), (int)(Position.Y + Size.Height / 2), Math.Max(Size.Width, Size.Height), Color);
        break;
    }
  }
}