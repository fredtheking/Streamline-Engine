using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Etc.Templates;

public abstract class CustomItemBehaviorTemplate : UuidIdentifier, IScript
{
  public Item Parent { get; set; }

  public virtual void Init(Context context) { }
  public virtual void CheckInitCorrect(Context context) { }
  public virtual void Enter(Context context) { }
  public virtual void Leave(Context context) { }
  public virtual void EarlyUpdate(Context context) { }
  public virtual void Update(Context context) { }
  public virtual void LateUpdate(Context context) { }
  public virtual void Draw(Context context) { }
  public virtual void DebugDraw(Context context) { }
}