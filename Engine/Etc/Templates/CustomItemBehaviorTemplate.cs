using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Etc.Templates;

public abstract class CustomItemBehaviorTemplate : IScript
{
  public Item Parent { get; set; }

  public virtual void Init(Context context) { }
  public virtual void Enter(Context context) { }
  public virtual void Leave(Context context) { }
  public virtual void Update(Context context) { }
  public virtual void Draw(Context context) { }
}