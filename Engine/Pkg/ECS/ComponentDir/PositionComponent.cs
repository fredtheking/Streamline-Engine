using System.Numerics;
using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class PositionComponent : ComponentGroup
{
  public int X { get; set; }
  public int Y { get; set; }
  public Vector2 Vec2 => new(X, Y);
  
  public PositionComponent() { X = (int)(Config.WindowSize.X / 2); Y = (int)(Config.WindowSize.Y / 2); }
  public PositionComponent(Vector2 pos) { X = (int)(pos.X / 2); Y = (int)(pos.Y / 2); }
  public PositionComponent(int x, int y) { X = x; Y = y; }
  public PositionComponent(int xy) { X = xy; Y = xy; }
  public PositionComponent(float x, float y) { X = (int)x; Y = (int)y; }
  public PositionComponent(float xy) { X = (int)xy; Y = (int)xy; }
}