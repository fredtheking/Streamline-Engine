using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.FolderItem;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

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
  public bool Hover { get; private set; } = false;
  public bool[] Click { get; private set; } = [false, false, false];
  public bool[] Press { get; private set; } = [false, false, false];
  public bool[] Release { get; private set; } = [false, false, false];
  public bool[] Hold { get; private set; } = [false, false, false];
  public bool[] Drag { get; private set; } = [false, false, false];
  private bool ColorInit;

  public MouseHitboxComponent() { ColorInit = true; }
  public MouseHitboxComponent(Color debugColor) { Color = debugColor; }

  public override void Init(Context context)
  {
    if (ColorInit) Color = Defaults.DebugHitboxColor;
    
    Item item = context.Managers.Item.GetByComponent(this);
    Position = item.Component<PositionComponent>() ?? Error(new PositionComponent(), "Item has no position component. Initialising default position.");
    Size = item.Component<SizeComponent>() ?? Error(new SizeComponent(), "Item has no size component. Initialising default size.");
    Figure = item.Component<FigureComponent>() ?? Error(new FigureComponent(), "Item has no figure component. Initialising default figure.");
    Border = Figure.Type == FigureType.Rounded ? item.Component<BorderComponent>() ?? new BorderComponent(0, Color.Blank) : new BorderComponent(0, Color.Blank);
    item.LocalLateInit(this);
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
          Error("Item's size is not square. Hitbox is now stretched to circle. Be careful!", true);
        return Raylib.CheckCollisionPointCircle(Raylib.GetMousePosition(), new Vector2(Position.X + LocalPosition.X + Size.Width / 2 + LocalSize.Width / 2, Position.Y + LocalPosition.Y + Size.Height / 2 + LocalSize.Height / 2), Math.Max(Size.Width + LocalSize.Width, Size.Height + LocalSize.Height));
    }
    return false;
  }

  public override void Update(Context context)
  {
    for (int i = 0; i < Click.Length; i++)
    {
      Hover = DecideHover();
      Click[i] = Raylib.IsMouseButtonPressed((MouseButton)i);
      Press[i] = Click[i] && Hover;
      Release[i] = Raylib.IsMouseButtonReleased((MouseButton)i);
      Hold[i] = Raylib.IsMouseButtonPressed((MouseButton)i);
      if (!Drag[i] & Click[i] & Hover) 
        Drag[i] = true;
      if (Drag[i] & !Hold[i]) 
        Drag[i] = false;
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