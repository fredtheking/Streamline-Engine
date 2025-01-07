using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class FigureComponent : ComponentTemplate
{
  public FigureType Type { get; set; }
  public float Roundness { get; set; }
  
  public FigureComponent() { Type = Defaults.Figure; Roundness = 1f; } 
  public FigureComponent(FigureType type) { Type = type; Roundness = 1f; } 
  public FigureComponent(FigureType type, float roundness) { Type = type; Roundness = roundness; } 
}