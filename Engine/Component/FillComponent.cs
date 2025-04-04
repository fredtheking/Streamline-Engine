using ImGuiNET;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Object;
using Color = Raylib_cs.Color;
using Rectangle = Raylib_cs.Rectangle;

namespace StreamlineEngine.Engine.Component;

public class FillComponent : ComponentTemplate, ICloneable<FillComponent>
{
  public PositionComponent LocalPosition { get; set; }
  public PositionComponent Position { get; set; }
  public SizeComponent LocalSize { get; set; }
  public SizeComponent Size { get; set; }
  public FigureComponent Figure { get; set; }
  public Color Color { get; set; }

  public FillComponent(Color color) { Color = color; DebugBorderColor = Color.White; }
  public FillComponent() { Color = Color.White; DebugBorderColor = Color.White; }

  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      PositionComponent? position = Parent.ComponentTry<PositionComponent>();
      if (position is null)
      {
        Warning(context, "Item has no position component. Initialising default position.");
        Position = new PositionComponent();
        Parent.AddComponents(Position);
        Position.Init(context);
      }
      else
      {
        Information(context, "Found position component!");
        Position = position;
      }
      
      SizeComponent? size = Parent.ComponentTry<SizeComponent>();
      if (size is null)
      {
        Warning(context, "Item has no size component. Initialising default size.");
        Size = new SizeComponent();
        Parent.AddComponents(Size);
        Size.Init(context);
      }
      else
      {
        Information(context, "Found size component!");
        Size = size;
      }
      
      FigureComponent? figure = Parent.ComponentTry<FigureComponent>();
      if (figure is null)
      {
        Warning(context, "Item has no figure component. Initialising default figure.");
        Figure = new FigureComponent();
        Parent.AddComponents(Figure);
        Figure.Init(context);
      }
      else
      {
        Information(context, "Found figure component!");
        Figure = figure;
      }
      
      Parent.LocalPosSizeToLateInit(this);
    });
  }

  public override void Draw(Context context)
  {
    switch (Figure.Type)
    {
      case FigureType.Rectangle:
        Raylib.DrawRectangleV(Position.Vec2 + LocalPosition.Vec2, Size.Vec2 + LocalSize.Vec2, Color);
        break;
      case FigureType.Rounded:
        Raylib.DrawRectangleRounded(new Rectangle(Position.Vec2 + LocalPosition.Vec2, Size.Vec2 + LocalSize.Vec2), Figure.Roundness, Defaults.RoundedSegments, Color);
        break;
      case FigureType.Circle:
        Raylib.DrawEllipse((int)(Position.X + LocalPosition.X + Size.Width / 2 + LocalSize.Width / 2), (int)(Position.Y + LocalPosition.Y + Size.Height / 2 + LocalSize.Height / 2), Size.Width + LocalSize.Width, Size.Height + LocalSize.Height, Color);
        break;
    }
  }
  
  public FillComponent Clone() => (FillComponent)MemberwiseClone();

  public override void DebuggerInfo(Context context)
  {
    base.DebuggerInfo(context);
    Extra.TransformImGuiInfo(Position, Size, Color, LocalPosition, LocalSize);
    ImGui.Separator();
    Extra.LinkToAnotherObjectImGui(context, "Figure", Figure);
  }
}