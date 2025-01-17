using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class FigureComponent : ComponentTemplate
{
  public FigureType Type { get; set; }
  public float Roundness { get; set; }
  
  public FigureComponent() { Type = MainContext.Const.Figure; Roundness = 1f; } 
  public FigureComponent(FigureType type) { Type = type; Roundness = 1f; } 
  public FigureComponent(FigureType type, float roundness) { Type = type; Roundness = roundness; } 
}