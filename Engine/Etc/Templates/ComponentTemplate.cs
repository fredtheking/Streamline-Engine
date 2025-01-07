using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Pkg.Etc.Templates;

public abstract class ComponentTemplate : UuidIdentifier, IScript
{
  public virtual void Init(GameContext context) { }
  public virtual void Enter(GameContext context) { }
  public virtual void Leave(GameContext context) { }
  public virtual void Update(GameContext context) { }
  public virtual void Draw(GameContext context) { }
}