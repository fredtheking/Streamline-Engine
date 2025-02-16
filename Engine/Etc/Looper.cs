namespace StreamlineEngine.Engine.Etc;

#if !RESOURCES
public static class Looper
{
  public static void Init(Context context)
  {
    context.Root.Init(context);
    context.Global.Init(context);
    if (context.Managers.Debug.TurnedOn) context.Debugger.Init(context);
    #if !RESOURCES
    context.Root.Change(context, Config.StartScene);
    #endif
  }
  public static void CheckInitCorrect(Context context)
  {
    context.Root.CheckInitCorrect(context);
    context.Global.CheckInitCorrect(context);
    if (context.Managers.Debug.TurnedOn) context.Debugger.CheckInitCorrect(context);
  }
  public static void Enter(Context context)
  {
    context.Root.Enter(context);
    context.Global.Enter(context);
    if (context.Managers.Debug.TurnedOn) context.Debugger.Enter(context);
  }
  public static void Leave(Context context)
  {
    context.Root.Leave(context);
    context.Global.Leave(context);
    if (context.Managers.Debug.TurnedOn) context.Debugger.Leave(context);
  }
  public static void EarlyUpdate(Context context)
  {
    context.Root.EarlyUpdate(context);
    context.Global.EarlyUpdate(context);
    if (context.Managers.Debug.TurnedOn) context.Debugger.EarlyUpdate(context);
  }
  public static void Update(Context context)
  {
    context.Root.Update(context);
    context.Global.Update(context);
    if (context.Managers.Debug.TurnedOn) context.Debugger.Update(context);
  }
  public static void LateUpdate(Context context)
  {
    context.Root.LateUpdate(context);
    context.Global.LateUpdate(context);
    if (context.Managers.Debug.TurnedOn) context.Debugger.LateUpdate(context);
  }
  public static void Draw(Context context)
  {
    context.Root.Draw(context);
    context.Global.Draw(context);
    context.Debugger.Draw(context);
  }
}
#endif