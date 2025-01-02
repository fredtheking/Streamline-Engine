using System.Numerics;
using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class SizeComponent : ComponentGroup
{
  public int Width { get; set; }
  public int Height { get; set; }
  public Vector2 Vec2 => new(Width, Height); 
  
  public SizeComponent() { Width = 100; Height = 100; }
  public SizeComponent(Vector2 size) { Width = (int)(size.X / 2); Height = (int)(size.Y / 2); }
  public SizeComponent(int w, int h) { Width = w; Height = h; }
  public SizeComponent(int wh) { Width = wh; Height = wh; }
  public SizeComponent(float w, float h) { Width = (int)w; Height = (int)h; }
  public SizeComponent(float wh) { Width = (int)wh; Height = (int)wh; }
}