using System.Numerics;
using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;

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

  public override void Init(MainContext context)
  {
    if (!InitWait) return;
    StaticEntity staticEntity = context.Managers.Entity.GetByComponent(this);
    
    X = Defaults.Position.X;
    Y = Defaults.Position.Y;
    
    SizeComponent? size = staticEntity.Component<SizeComponent>();
    if (size is null) return;
    
    size.Init(context);
    Information("Found size component! Using size to center position.");
    X -= size.Width / 2;
    Y -= size.Height / 2;
  }
  
  public PositionComponent Clone() => (PositionComponent)MemberwiseClone();
}