using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Pkg.Etc.Templates;

public class MaterialTemplate<TFilename, TMaterial> : UuidIdentifier, IMaterial
{
  public TFilename? Filename { get; protected set; }
  public TMaterial? Material { get; protected set; }

  public virtual bool Ready() => Critical(false, "One of material's 'Ready' functions is not implemented! Returning only false.", true);
  public virtual void Init(MainContext context) { }
  public virtual void Enter(MainContext context) { }
  public virtual void Leave(MainContext context) { }
  public virtual void Update(MainContext context) { }
  public virtual void Draw(MainContext context) { }
}