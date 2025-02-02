using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Component;

public class MouseHitboxComponent : ComponentTemplate, ICloneable<MouseHitboxComponent>
{
  public PositionComponent LocalPosition { get; set; }
  public PositionComponent Position { get; set; }
  public SizeComponent LocalSize { get; set; }
  public SizeComponent Size { get; set; }
  public FigureComponent Figure { get; set; }
  public BorderComponent Border { get; set; }
  public Color Color { get; set; }
  /// <summary>
  /// Returns true when hovering over hitbox with your mouse
  /// </summary>
  public bool Hover { get; private set; }
  /// <summary>
  /// Returns true when clicking with mouse button (ANY click detected)
  /// </summary>
  public bool[] Press { get; private set; } = [false, false, false];
  /// <summary>
  /// Returns true when clicking with mouse button (ONLY on the hitbox)
  /// </summary>
  public bool[] Click { get; private set; } = [false, false, false];
  /// <summary>
  /// Returns true when released mouse button
  /// </summary>
  public bool[] Release { get; private set; } = [false, false, false];
  /// <summary>
  /// Returns true when mouse button is down (ANY hold detected)
  /// </summary>
  public bool[] Down { get; private set; } = [false, false, false];
  /// <summary>
  /// Returns true when mouse button is down (ONLY on the hitbox)
  /// </summary>
  public bool[] Hold { get; private set; } = [false, false, false];
  /// <summary>
  /// Returns true when mouse button is dragged (Hold starting in hitbox only, can be released outside hitbox)
  /// </summary>
  public bool[] Drag { get; private set; } = [false, false, false];
  private bool ColorInit;

  public MouseHitboxComponent() { ColorInit = true; }
  public MouseHitboxComponent(Color debugColor) { Color = debugColor; }

  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      if (ColorInit) Color = Defaults.DebugHitboxColor;
    
      Item item = context.Managers.Item.GetByComponent(this);
      Position = item.ComponentTry<PositionComponent>() ?? Error(context, new PositionComponent(), "Item has no position component. Initialising default position.");
      Size = item.ComponentTry<SizeComponent>() ?? Error(context, new SizeComponent(), "Item has no size component. Initialising default size.");
      Figure = item.ComponentTry<FigureComponent>() ?? Error(context, new FigureComponent(), "Item has no figure component. Initialising default figure.");
      Border = Figure.Type == FigureType.Rounded ? item.ComponentTry<BorderComponent>() ?? new BorderComponent(0, Color.Blank) : new BorderComponent(0, Color.Blank);
      item.LocalLatePosSizeInit(this);

      switch (Figure.Type)
      {
        case FigureType.Rounded:
          Warning(context, "Hitbox on rounded figure works as same as rectangle ones. Be careful with corners!");
          break;
        case FigureType.Circle:
          if (Math.Abs(Size.Width - Size.Height) > 0)
            Error(context, "Item's size is not square. Hitbox is now stretched to circle. Be careful!");
          break;
      }
    });
  }

  private bool DecideHover()
  {
    switch (Figure.Type)
    {
      case FigureType.Rectangle:
        return Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(Position.Vec2, Size.Vec2));
      case FigureType.Rounded:
        return Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(Position.Vec2 - new Vector2(Border.Thickness), Size.Vec2 + new Vector2(Border.Thickness * 2)));
      case FigureType.Circle:
        return Raylib.CheckCollisionPointCircle(Raylib.GetMousePosition(), new Vector2(Position.X + LocalPosition.X + Size.Width / 2 + LocalSize.Width / 2, Position.Y + LocalPosition.Y + Size.Height / 2 + LocalSize.Height / 2), Math.Max(Size.Width + LocalSize.Width, Size.Height + LocalSize.Height));
    }
    return false;
  }

  public override void Enter(Context context)
  {
    Hover = false;
    Press = [false, false, false];
    Click = [false, false, false];
    Release = [false, false, false];
    Down = [false, false, false];
    Hold = [false, false, false];
    Drag = [false, false, false];
  }

  public override void Update(Context context)
  {
    Hover = DecideHover();
    for (int i = 0; i < Press.Length; i++)
    {
      Press[i] = Raylib.IsMouseButtonPressed((MouseButton)i);
      Click[i] = Press[i] && Hover;
      Release[i] = Raylib.IsMouseButtonReleased((MouseButton)i);
      Down[i] = Raylib.IsMouseButtonDown((MouseButton)i);
      Hold[i] = Down[i] && Hover;
      if (!Drag[i] & Click[i]) Drag[i] = true;
      if (Drag[i] & !Down[i]) Drag[i] = false;
    }
  }

  public override void Draw(Context context)
  {
    if (!context.Managers.Debug.TurnedOn) return;

    switch (Figure.Type)
    {
      case FigureType.Rectangle or FigureType.Rounded:
        Raylib.DrawRectangleV(Position.Vec2 + LocalPosition.Vec2 - new Vector2(Border.Thickness), Size.Vec2  + LocalSize.Vec2 + new Vector2(Border.Thickness * 2), Color);
        break;
      case FigureType.Circle:
        Raylib.DrawCircle((int)(Position.X + LocalPosition.X + Size.Width / 2 + LocalSize.Width / 2), (int)(Position.Y + LocalPosition.Y + Size.Height / 2 + LocalSize.Height / 2), Math.Max(Size.Width + LocalSize.Width, Size.Height + LocalSize.Height), Color);
        break;
    }
  }
  
  public MouseHitboxComponent Clone() => (MouseHitboxComponent)MemberwiseClone();
}