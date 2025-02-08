using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Etc.Templates;

public abstract class ComponentTemplate : UuidIdentifier, IScript
{
  protected bool Initialized { get; private set; }

  protected void InitOnce(Action action)
  {
    if (Initialized) return;
    action();
    Initialized = true;
  }
  public virtual void Init(Context context) { }
  public virtual void Enter(Context context) { }
  public virtual void Leave(Context context) { }
  public virtual void EarlyUpdate(Context context) { }
  public virtual void Update(Context context) { }
  public virtual void LateUpdate(Context context) { }
  public virtual void PreDraw(Context context) { }
  public virtual void Draw(Context context) { }
}