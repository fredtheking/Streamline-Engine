using Raylib_cs;
using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Manager;

namespace StreamlineEngine.Engine;

public class GameContext
{
  public Managers Managers = new();

  public void Run()
  {
    Init();
    Loop();
    Close();
  }
  
  private void Init()
  {
    Raylib.SetConfigFlags(Config.WindowConfigFlags);
    Raylib.InitWindow((int)Config.WindowSize.X, (int)Config.WindowSize.Y, Config.WindowTitle);
    Raylib.InitAudioDevice();
    Registration.EntitiesCreation(this);
    Registration.MaterialsCreation(this);
    foreach (StaticEntity entity in Managers.Entity.All.Values)
      entity.Init(this);
  }

  private void Loop()
  {
    while (!Raylib.WindowShouldClose())
    {
      Raylib.BeginDrawing();
      Raylib.ClearBackground(Config.WindowBackgroundColor);
      
      if (Managers.Scene.Changed) MainLoop.Enter(this);
      MainLoop.Update(this);
      MainLoop.Draw(this);
      
      Raylib.EndDrawing();
    }
  }

  private void Close()
  {
    Managers.Scene.Current?.Leave(this);
    Raylib.CloseWindow();
    Raylib.CloseAudioDevice();
  }
}