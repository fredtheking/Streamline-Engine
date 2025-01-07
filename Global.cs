using Raylib_cs;
using StreamlineEngine.Engine;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine;

public class Global : IScript
{
  public void Init(GameContext context) { }
  public void Enter(GameContext context) { }
  public void Leave(GameContext context) { }

  public void Update(GameContext context)
  {
    #if DEBUG
    if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.F1)) 
      context.Managers.Scene.Previous(context);
    if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.F2)) 
      context.Managers.Scene.Next(context);
    if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.F3)) 
      context.Managers.Debug.Toggle();
    #endif
  }

  public void Draw(GameContext context)
  {
    Raylib.DrawFPS(10, 10);
  }
}