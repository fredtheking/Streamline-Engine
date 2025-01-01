using System.Numerics;
using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.ECS.Component;

public class SizeCom : ComponentGroup, IScript
{
  public int Width { get; set; }
  public int Height { get; set; }
  
  public SizeCom() { Width = (int)(Config.WindowSize.X / 2); Height = (int)(Config.WindowSize.Y / 2); }
  public SizeCom(Vector2 size) { Width = (int)(size.X / 2); Height = (int)(size.Y / 2); }
  public SizeCom(int w, int h) { Width = w; Height = h; }
  public SizeCom(int wh) { Width = wh; Height = wh; }
  public SizeCom(float w, float h) { Width = (int)w; Height = (int)h; }
  public SizeCom(float wh) { Width = (int)wh; Height = (int)wh; }
}