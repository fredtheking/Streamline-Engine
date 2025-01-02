using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class LayerComponent : ComponentGroup
{
  public int Layer { get; set; }

  public LayerComponent(int layer) => Layer = layer;
}