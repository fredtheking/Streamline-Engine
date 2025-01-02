using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public abstract class ComponentGroup : UuidIdentifier, IScript
{
  public virtual void Init(GameContext context) { }
  public virtual void Enter(GameContext context) { }
  public virtual void Update(GameContext context) { }
  public virtual void Draw(GameContext context) { }
}