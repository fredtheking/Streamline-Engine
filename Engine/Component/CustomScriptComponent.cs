using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class CustomScriptComponent : ComponentTemplate
{
  public dynamic Script { get; private set; }
  
  public CustomScriptComponent(dynamic script) => Script = script;

  public override void Init(MainContext context) =>
    Script.Init(context);
  
  public override void Enter(MainContext context) =>
    Script.Enter(context);
  
  public override void Update(MainContext context) =>
    Script.Update(context);
  
  public override void Draw(MainContext context) =>
    Script.Draw(context);
}