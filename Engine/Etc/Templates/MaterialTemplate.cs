using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Etc.Templates;

public class MaterialTemplate : UuidIdentifier
{
  public int Id { get; protected set; }
  public dynamic? Material { get; protected set; }
  public bool LoadOnNeed { get; protected init; }
  public virtual bool Ready() => throw new NotOverriddenException("'Ready' function is not overridden!");
  
  public virtual void Load(Context context) => throw new NotOverriddenException("'Load' function is not overridden!");
  public virtual void Unload(Context context) => throw new NotOverriddenException("'Unload' function is not overridden!");
  public virtual void Init(Context context) { }

  public virtual void Enter(Context context)
  {
    if (Ready() || LoadOnNeed) return;
    Load(context);
  }

  public virtual void Leave(Context context)
  {
    if (!Ready()) return;
    Unload(context);
  }
  public void Update(Context context) =>
    throw new CallNotAllowedException("Should not be called");
  public void Draw(Context context) =>
    throw new CallNotAllowedException("Should not be called");
}