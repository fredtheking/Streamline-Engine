namespace StreamlineEngine.Engine.Etc;

public static class MainLoop
{
  public static void Enter(GameContext context)
  {
    context.Managers.Scene.Changed = false;
    context.Managers.Scene.Current?.Enter(context);
    context.Global.Enter(context);
  }
  
  public static void Update(GameContext context)
  {
    context.Managers.Scene.Current?.Update(context);
    context.Global.Update(context);
  }
  
  public static void Draw(GameContext context)
  {
    context.Managers.Scene.Current?.Draw(context);
    context.Global.Draw(context);
  }
}