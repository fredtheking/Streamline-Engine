using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class LayerComponent : ComponentTemplate
{
  public int Layer { get; set; }
  
  public LayerComponent(int layer = 0) => Layer = layer;
}