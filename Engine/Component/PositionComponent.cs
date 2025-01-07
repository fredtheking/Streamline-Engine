using System.Numerics;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class PositionComponent : ComponentTemplate
{
  public float X { get; set; }
  public float Y { get; set; }
  public Vector2 Vec2 => new(X, Y);
  
  public PositionComponent() { X = Defaults.Position.X; Y = Defaults.Position.Y; }
  public PositionComponent(Vector2 pos) { X = pos.X / 2; Y = pos.Y / 2; }
  public PositionComponent(float x, float y) { X = x; Y = y; }
  public PositionComponent(float xy) { X = xy; Y = xy; }
}