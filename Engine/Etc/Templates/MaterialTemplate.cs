using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Pkg.Etc.Templates;

public class MaterialTemplate<TFilename, TMaterial> : UuidIdentifier, IScript, IMaterial
{
  public TFilename? Filename { get; protected set; }
  public TMaterial? Material { get; protected set; }

  public virtual bool Ready() => Critical(false, "One of material's 'Ready' functions is not implemented! Returning only false.", true);
  public virtual void Init(GameContext context) { }
  public virtual void Enter(GameContext context) { }
  public virtual void Leave(GameContext context) { }
  public virtual void Update(GameContext context) { }
  public virtual void Draw(GameContext context) { }
}