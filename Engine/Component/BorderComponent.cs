using ImGuiNET;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Object;

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
  
  /// <summary>
  /// Some of the components require this <c>BorderComponent</c> to be. This variable determines whether its normal or junk (declared as a real component or a part of other component) 
  /// </summary>
  public bool Junk = false;

  public BorderComponent()
  {
    Thickness = 1f;
    Color = Color.LightGray;
    DebugBorderColor = Color.Blank;

    if (Junk)
    {
      Thickness = 0f;
      Color = Color.Blank;
    }
  }
  public BorderComponent(Color color)
  {
    Thickness = 1f;
    Color = color;
    DebugBorderColor = Color.Blank;
  }
  public BorderComponent(float thickness)
  {
    Thickness = thickness;
    Color = Color.LightGray;
    DebugBorderColor = Color.Blank;
  }
  public BorderComponent(float thickness, Color color)
  {
    Thickness = thickness;
    Color = color;
    DebugBorderColor = Color.Blank;
  }

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
        Raylib.DrawRectangleLinesEx(new Rectangle(Position.Vec2 + LocalPosition.Vec2, Size.Vec2 + LocalSize.Vec2), Thickness, Color);
        break;
      case FigureType.Rounded:
        Raylib.DrawRectangleRoundedLinesEx(new Rectangle(Position.Vec2 + LocalPosition.Vec2, Size.Vec2 + LocalSize.Vec2), Figure.Roundness, Defaults.RoundedSegments, Thickness, Color);
        break;
      case FigureType.Circle:
        Raylib.DrawEllipseLines((int)(Position.X + LocalPosition.X + Size.Width / 2 + LocalSize.Width / 2), (int)(Position.Y + LocalPosition.Y + Size.Height / 2 + LocalSize.Height / 2), Size.Width + LocalSize.Width, Size.Height + LocalSize.Height, Color);
        break;
    }
  }

  public override void DebuggerInfo(Context context)
  {
    base.DebuggerInfo(context);
    ImGui.Text($"Position: {Position.Vec2}  +  {LocalPosition.Vec2}");
    ImGui.Text($"Size: {Size.Vec2}  +  {LocalSize.Vec2}");
    Extra.ColorToColoredImGuiText(Color);
    ImGui.Separator();
    ImGui.Text("Figure:");
    ImGui.SameLine();
    if (ImGui.SmallButton(Figure.ShortUuid))
      context.Debugger.CurrentTreeInfo.Add(Figure.DebuggerInfo);
    ImGui.Text($"Thickness: {Thickness}");
  }
}