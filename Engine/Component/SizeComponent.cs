using System.Numerics;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class SizeComponent : ComponentTemplate
{
  public float Width { get; set; }
  public float Height { get; set; }
  public Vector2 Vec2 => new(Width, Height); 
  
  public SizeComponent() { Width = Defaults.Size.X; Height = Defaults.Size.Y; }
  public SizeComponent(Vector2 size) { Width = size.X / 2; Height = size.Y / 2; }
  public SizeComponent(float w, float h) { Width = w; Height = h; }
  public SizeComponent(float wh) { Width = wh; Height = wh; }
}