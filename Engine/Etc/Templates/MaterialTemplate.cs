using StreamlineEngine.Engine.Etc.Exceptions;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Etc.Templates;

public class MaterialTemplate<TFilename, TMaterial> : UuidIdentifier, IMaterial
{
  public TFilename? Id { get; protected set; }
  public TMaterial? Material { get; protected set; }
  public virtual bool Ready() => throw new NotOverriddenException("'Ready' function is not overridden!");
  
  public virtual void Init(Context context) { }
  public virtual void Enter(Context context) { }
  public virtual void Leave(Context context) { }
  public void Update(Context context) =>
    throw new Exception("Should not be called");
  public void Draw(Context context) =>
    throw new Exception("Should not be called");
}