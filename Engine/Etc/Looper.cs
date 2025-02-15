namespace StreamlineEngine.Engine.Etc;

#if !RESOURCES
public static class Looper
{
  public static void Init(Context context)
  {
    context.Root.Init(context);
    context.Global.Init(context);
    #if !RESOURCES
    context.Root.Change(context, Config.StartScene);
    #endif
  }
  public static void CheckInitCorrect(Context context)
  {
    context.Root.CheckInitCorrect(context);
    context.Global.CheckInitCorrect(context);
  }
  public static void Enter(Context context)
  {
    context.Root.Enter(context);
    context.Global.Enter(context);
  }
  public static void Leave(Context context)
  {
    context.Root.Leave(context);
    context.Global.Leave(context);
  }
  public static void EarlyUpdate(Context context)
  {
    context.Root.EarlyUpdate(context);
    context.Global.EarlyUpdate(context);
  }
  public static void Update(Context context)
  {
    context.Root.Update(context);
    context.Global.Update(context);
  }
  public static void LateUpdate(Context context)
  {
    context.Root.LateUpdate(context);
    context.Global.LateUpdate(context);
  }
  public static void PreDraw(Context context)
  {
    context.Root.PreDraw(context);
    context.Global.PreDraw(context);
  }
  public static void Draw(Context context)
  {
    context.Root.Draw(context);
    context.Global.Draw(context);
  }
}
#endif