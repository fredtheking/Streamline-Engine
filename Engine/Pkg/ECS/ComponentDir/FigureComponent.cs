using StreamlineEngine.Engine.Pkg.Etc;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class FigureComponent : ComponentGroup
{
  public FigureType Type { get; set; }
  public float Roundness { get; set; }
  
  public FigureComponent() { Type = FigureType.Rectangle; Roundness = 1f; } 
  public FigureComponent(FigureType type) { Type = type; Roundness = 1f; } 
  public FigureComponent(FigureType type, float roundness) { Type = type; Roundness = roundness; } 
}