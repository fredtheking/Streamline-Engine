using Raylib_cs;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Etc.Templates;

public abstract class ComponentTemplate : UuidIdentifier, IScript
{
  public Color DebugBorderColor { get; protected init; } = Color.Pink;
  public virtual void Init(Context context) { Initialized = true; }
  public virtual void CheckInitCorrect(Context context) { if (!Initialized) throw new NotInitialisedException(); }
  public virtual void Enter(Context context) { }
  public virtual void Leave(Context context) { }
  public virtual void EarlyUpdate(Context context) { }
  public virtual void Update(Context context) { }
  public virtual void LateUpdate(Context context) { }
  public virtual void PreDraw(Context context) { }
  public virtual void Draw(Context context) { }
  public virtual void DebugDraw(Context context) { }
}