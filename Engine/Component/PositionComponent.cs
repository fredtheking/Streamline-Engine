using System.Numerics;
using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class PositionComponent : ComponentTemplate
{
  public float X { get; set; }
  public float Y { get; set; }
  public Vector2 Vec2 => new(X, Y);
  private bool InitWait;

  public PositionComponent() { InitWait = true; }
  public PositionComponent(Vector2 pos) { X = pos.X / 2; Y = pos.Y / 2; }
  public PositionComponent(float x, float y) { X = x; Y = y; }
  public PositionComponent(float xy) { X = xy; Y = xy; }

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
}