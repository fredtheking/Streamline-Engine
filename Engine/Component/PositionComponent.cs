using System.Numerics;
using ImGuiNET;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Component;

public class PositionComponent : ComponentTemplate, ICloneable<PositionComponent>
{
  public float X { get; set; }
  public float Y { get; set; }
  public Vector2 Vec2 => new(X, Y);
  private bool InitWait;

  public PositionComponent() { InitWait = true; }
  public PositionComponent(Vector2 pos) { X = pos.X / 2; Y = pos.Y / 2; }
  public PositionComponent(float x, float y) { X = x; Y = y; }
  public PositionComponent(float xy) { X = xy; Y = xy; }
  
  public void Set(Vector2 pos) { X = pos.X; Y = pos.Y; }
  public void Set(float x, float y) { X = x; Y = y; }
  public void Set(float xy) { X = xy; Y = xy; }
  public void Add(Vector2 pos) { X += pos.X; Y += pos.Y; }
  public void Add(float x, float y) { X += x; Y += y; }
  public void Add(float xy) { X += xy; Y += xy; }

  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      if (!InitWait) return;

      X = Defaults.Position.X;
      Y = Defaults.Position.Y;
      
      SizeComponent? size = Parent.ComponentTry<SizeComponent>();
      if (size is null)
      {
        Warning(context, "Item has no size component. Initialising default size.");
        size = new SizeComponent();
        Parent.AddComponents(size);
        size.Init(context);
      }

      Information(context, "Found size component! Using size to center position.");
      size.Init(context);
      X -= size.Width / 2;
      Y -= size.Height / 2;
    });
  }
  
  public PositionComponent Clone() => (PositionComponent)MemberwiseClone();

  public override void DebuggerInfo(Context context)
  {
    base.DebuggerInfo(context);
    ImGui.Text($"X: {X}");
    ImGui.Text($"Y: {Y}");
  }
}