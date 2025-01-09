namespace StreamlineEngine.Engine.Etc;

public static class MainLoop
{
  public static void Enter(MainContext context)
  {
    context.Global.Enter(context);
    context.Managers.Scene.Changed = false;
    context.Managers.Scene.Current?.Enter(context);
  }
  
  public static void Update(MainContext context)
  {
    context.Global.Update(context);
    context.Managers.Scene.Current?.Update(context);
  }
  
  public static void Draw(MainContext context)
  {
    context.Managers.Scene.Current?.Draw(context);
    context.Global.Draw(context);
  }
}