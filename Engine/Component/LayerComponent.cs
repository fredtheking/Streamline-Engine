using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class LayerComponent : ComponentTemplate
{
  public int Layer { get; set; }

  public LayerComponent(int layer) => Layer = layer;
}