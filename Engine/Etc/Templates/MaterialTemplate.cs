using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Pkg.Etc.Templates;

public class MaterialTemplate<TFilename, TMaterial> : UuidIdentifier, IMaterial
{
  public TFilename? Id { get; protected set; }
  public TMaterial? Material { get; protected set; }

  public virtual bool Ready() => Critical(false, "One of material's 'Ready' functions is not implemented! Returning only false.", true);
  
  public virtual void Init(Context context) { }
  public virtual void Enter(Context context) { }
  public virtual void Leave(Context context) { }
  public void Update(Context context) =>
    throw new Exception("Should not be called");
  public void Draw(Context context) =>
    throw new Exception("Should not be called");
}