using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.ECS.ComponentDir;

public class CustomScriptComponent : ComponentGroup
{
  public dynamic Script { get; private set; }
  
  public CustomScriptComponent(dynamic script) => Script = script;

  public override void Init(GameContext context) =>
    Script.Init(context);
  
  public override void Enter(GameContext context) =>
    Script.Enter(context);
  
  public override void Update(GameContext context) =>
    Script.Update(context);
  
  public override void Draw(GameContext context) =>
    Script.Draw(context);
}