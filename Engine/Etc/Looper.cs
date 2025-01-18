using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Etc;

public static class Looper
{
  public static void Init(Context context)
  {
    context.Root.Init(context);
    context.Global.Init(context);
    context.Root.Change(context, Config.StartScene);
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
  
  public static void Update(Context context)
  {
    context.Root.Update(context);
    context.Global.Update(context);
  }
  
  public static void Draw(Context context)
  {
    context.Root.Draw(context);
    context.Global.Draw(context);
  }
}