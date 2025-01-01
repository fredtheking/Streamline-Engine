using Raylib_cs;

namespace StreamlineEngine.Engine.Pkg;

public class GameContext
{
  public void Init()
  {
    Raylib.SetConfigFlags(Config.WindowConfigFlags);
    Raylib.InitWindow((int)Config.WindowSize.X, (int)Config.WindowSize.Y, Config.WindowTitle);
    Raylib.InitAudioDevice();
    Registration.Init();
  }

  public void Loop(Action action)
  {
    while (!Raylib.WindowShouldClose())
    {
      Raylib.BeginDrawing();
      Raylib.ClearBackground(Color.Black);
      action();
      Raylib.EndDrawing();
    }
  }

  public void Close()
  {
    Raylib.CloseWindow();
    Raylib.CloseAudioDevice();
  }
}