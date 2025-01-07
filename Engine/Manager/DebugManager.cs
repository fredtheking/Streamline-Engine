namespace StreamlineEngine.Engine.Pkg.Manager;

public class DebugManager
{
  public bool TurnedOn { get; set; } = Config.DebugMode;
  public bool Changed { get; set; } = Config.DebugMode;
  
  public void Toggle() {
    TurnedOn = !TurnedOn;
    Changed = true;
  }
}