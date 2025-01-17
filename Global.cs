using Raylib_cs;
using StreamlineEngine.Engine;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine;

public class Global : IScript
{
  public void Init(MainContext context) { }
  public void Enter(MainContext context) { }
  public void Leave(MainContext context) { }

  public void Update(MainContext context)
  {
    #if DEBUG
    //if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.F1)) 
    //  context.Managers.Scene.Previous(context);
    //if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.F2)) 
    //  context.Managers.Scene.Next(context);
    if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.F3)) 
      context.Managers.Debug.Toggle();
    #endif
  }

  public void Draw(MainContext context)
  {
    Raylib.DrawFPS(10, 10);
  }
}