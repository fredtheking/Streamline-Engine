using Raylib_cs;
using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class HitboxComponent : ComponentGroup
{
  public ObjectComponent Object { get; set; }
  public Color Color { get; set; }
  
  public HitboxComponent(ObjectComponent @object) { Object = @object; Color = Config.DebugHitboxColor; }
  public HitboxComponent(ObjectComponent @object, Color debugColor) { Object = @object; Color = debugColor; }
}