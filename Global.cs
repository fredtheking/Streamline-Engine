using Raylib_cs;
using StreamlineEngine.Engine;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine;

public class Global : IScript
{
  public void Init(Context context) { }
  public void CheckInitCorrect(Context context) { }
  public void Enter(Context context) { }
  public void Leave(Context context) { }
  public void EarlyUpdate(Context context) { }
  public void Update(Context context)
  {
    #if DEBUG
    if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.F1)) 
      context.Root.Previous(context);
    if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.F2)) 
      context.Root.Next(context);
    if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.F3)) 
      context.Managers.Debug.Toggle();
    #endif
  }
  public void LateUpdate(Context context) { }
  public void Draw(Context context)
  {
    Raylib.DrawFPS(10, 10);
  }
  public void DebugDraw(Context context) { }
}