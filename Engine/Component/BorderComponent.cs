using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.FolderItem;

namespace StreamlineEngine.Engine.Component;

public class BorderComponent : ComponentTemplate
{
  public PositionComponent LocalPosition { get; set; }
  public PositionComponent Position { get; set; }
  public SizeComponent LocalSize { get; set; }
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
    Item item = context.Managers.Item.GetByComponent(this);
    Position = item.Component<PositionComponent>() ?? Error(new PositionComponent(), "Entity has no position component. Initialising default position.");
    Size = item.Component<SizeComponent>() ?? Error(new SizeComponent(), "Entity has no size component. Initialising default size.");
    Figure = item.Component<FigureComponent>() ?? Error(new FigureComponent(), "Entity has no figure component. Initialising default figure.");
    
    item.LocalLateInit(this);
  }

  public override void Draw(MainContext context)
  {
    switch (Figure.Type)
    {
      case FigureType.Rectangle:
        Raylib.DrawRectangleLinesEx(new Rectangle(Position.Vec2 + LocalPosition.Vec2, Size.Vec2 + LocalSize.Vec2), Thickness, Color);
        break;
      case FigureType.Rounded:
        Raylib.DrawRectangleRoundedLinesEx(new Rectangle(Position.Vec2 + LocalPosition.Vec2, Size.Vec2 + LocalSize.Vec2), Figure.Roundness, MainContext.Const.RoundedSegments, Thickness, Color);
        break;
      case FigureType.Circle:
        Raylib.DrawEllipseLines((int)(Position.X + LocalPosition.X + Size.Width / 2 + LocalSize.Width / 2), (int)(Position.Y + LocalPosition.Y + Size.Height / 2 + LocalSize.Height / 2), Size.Width + LocalSize.Width, Size.Height + LocalSize.Height, Color);
        break;
    }
  }
}