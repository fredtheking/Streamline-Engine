namespace StreamlineEngine.Engine.Pkg.Manager;

public class Managers
{
  public SceneManager Scene { get; } = new();
  public EntityManager Entity { get; } = new();
  public DebugManager Debug { get; } = new();
  public ResourceManager Resource { get; } = new();
  public KeybindManager Keybind { get; } = new();
}