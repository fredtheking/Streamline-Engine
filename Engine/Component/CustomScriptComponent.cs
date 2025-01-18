using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class CustomScriptComponent : ComponentTemplate
{
  public dynamic Script { get; private set; }
  
  public CustomScriptComponent(dynamic script) => Script = script;

  public override void Init(Context context) =>
    Script.Init(context);
  
  public override void Enter(Context context) =>
    Script.Enter(context);
  
  public override void Update(Context context) =>
    Script.Update(context);
  
  public override void Draw(Context context) =>
    Script.Draw(context);
}