namespace StreamlineEngine.Engine.Etc;

public static class MainLoop
{
  public static void Enter(MainContext context)
  {
    context.Global.Enter(context);
    MainContext.Root.Changed = false;
    MainContext.Root.Enter(context);
  }
  
  public static void Update(MainContext context)
  {
    context.Global.Update(context);
    MainContext.Root.Update(context);
  }
  
  public static void Draw(MainContext context)
  {
    MainContext.Root.Draw(context);
    context.Global.Draw(context);
  }
}