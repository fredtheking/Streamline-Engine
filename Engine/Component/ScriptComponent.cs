using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class ScriptComponent : ComponentTemplate
{
  public CustomItemBehaviorTemplate Script { get; }
  
  public ScriptComponent(CustomItemBehaviorTemplate script) => Script = script;

  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      Script.Parent = context.Managers.Object.GetByComponent(this);
      Script.Init(context);
    });
  }
  
  public override void Enter(Context context) =>
    Script.Enter(context);
  
  public override void Leave(Context context) =>
    Script.Leave(context);
  
  public override void Update(Context context) =>
    Script.Update(context);
  
  public override void Draw(Context context) =>
    Script.Draw(context);
}