using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class LayerComponent : ComponentTemplate
{
  public RefObj<int> Layer { get; set; }
  
  public LayerComponent(int layer = 0) => Layer = new RefObj<int>(layer);
}