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
    if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.Grave)) 
      context.Debugger.Switch();
    #endif
  }
  public void LateUpdate(Context context) { }
  public void Draw(Context context)
  {
    #if DEBUG
    Raylib.DrawText($"Work in Progress [ {Config.Version} ] : This is not a final product. Build time: {Math.Round(Raylib.GetTime(), 1).ToString().PadRight(5, '_')}. Fps: {Raylib.GetFPS().ToString().PadLeft(4, '_')}. '~' for debugger.", 5, (int)(Config.WindowSize.Y - 20), 12, new Color(255, 255, 255, 69));
    #endif
  }
  public void DebugDraw(Context context) { }
}