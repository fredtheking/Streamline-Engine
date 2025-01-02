namespace StreamlineEngine.Engine.Pkg.Etc;

public static class MainLoop
{
  public static void Enter(GameContext context)
  {
    context.Managers.Scene.Current.Enter(context);
  }
  
  public static void Update(GameContext context)
  {
    context.Managers.Scene.Current.Update(context);
  }
  
  public static void Draw(GameContext context)
  {
    context.Managers.Scene.Current.Draw(context);
  }
}