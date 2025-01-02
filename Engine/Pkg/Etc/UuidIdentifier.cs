namespace StreamlineEngine.Engine.Pkg.Etc;

public class UuidIdentifier
{ 
  public string Uuid { get; protected set; } = Guid.NewGuid().ToString("D");
}