using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class LayerComponent : ComponentTemplate
{
  public int Layer { get; set; }

  public LayerComponent(int layer) => Layer = layer;
}