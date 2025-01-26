using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class ScriptComponent : ComponentTemplate
{
  public dynamic Script { get; private set; }
  
  public ScriptComponent(dynamic script) => Script = script;

  public override void Init(Context context)
  {
    Script.Parent = context.Managers.Item.GetByComponent(this);
    Script.Init(context);
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