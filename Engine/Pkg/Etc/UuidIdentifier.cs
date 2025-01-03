using StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

namespace StreamlineEngine.Engine.Pkg.Etc;

public class UuidIdentifier
{ 
  public string Uuid { get; protected set; } = Guid.NewGuid().ToString("D");
  
  protected T Warning<T>(T component, string message) where T : ComponentGroup { Console.WriteLine(message); return component; }
}