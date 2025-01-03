using Raylib_cs;
using StreamlineEngine.Engine.Pkg.ECS.EntityDir;
using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Manager;

namespace StreamlineEngine.Engine.Pkg;

public class GameContext
{
  public Dictionary<string, Entity> Entities = [];
  public Managers Managers = new();
  
  public void Init(GameContext context)
  {
    Raylib.SetConfigFlags(Config.WindowConfigFlags);
    Raylib.InitWindow((int)Config.WindowSize.X, (int)Config.WindowSize.Y, Config.WindowTitle);
    Raylib.InitAudioDevice();
    Registration.Init(context);
    foreach (Entity entity in Entities.Values)
      entity.Init(context);
  }

  public void Loop(GameContext context)
  {
    while (!Raylib.WindowShouldClose())
    {
      Raylib.BeginDrawing();
      Raylib.ClearBackground(Config.WindowBackgroundColor);
      
      if (Managers.Scene.Changed) MainLoop.Enter(context);
      MainLoop.Update(context);
      MainLoop.Draw(context);
      
      Raylib.EndDrawing();
    }
  }

  public void Close()
  {
    Raylib.CloseWindow();
    Raylib.CloseAudioDevice();
  }
}