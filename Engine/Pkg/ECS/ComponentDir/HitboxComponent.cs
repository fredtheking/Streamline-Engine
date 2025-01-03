using Raylib_cs;
using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class HitboxComponent : ComponentGroup
{
  public FillComponent Fill { get; set; }
  public Color Color { get; set; }
  
  public HitboxComponent(FillComponent fill) { Fill = fill; Color = Config.DebugHitboxColor; }
  public HitboxComponent(FillComponent fill, Color debugColor) { Fill = fill; Color = debugColor; }
}