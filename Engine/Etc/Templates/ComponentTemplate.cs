using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Etc.Templates;

public abstract class ComponentTemplate : UuidIdentifier, IScript
{
  public virtual void Init(MainContext context) { }
  public virtual void Enter(MainContext context) { }
  public virtual void Leave(MainContext context) { }
  public virtual void Update(MainContext context) { }
  public virtual void Draw(MainContext context) { }
}