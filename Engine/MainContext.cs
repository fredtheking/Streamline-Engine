using Raylib_cs;
using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Manager;

namespace StreamlineEngine.Engine;

public class MainContext
{
  public Managers Managers { get; } = new();
  public Global Global { get; } = new();
  
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
    Managers.Debug.PrintSeparator(ConsoleColor.Yellow, "Window and audio created. Starting entities and materials initialisation...");
    Registration.EntitiesCreation(this);
    Registration.MaterialsCreation(this);
    Managers.Debug.PrintSeparator(ConsoleColor.Yellow);
    foreach (StaticEntity entity in Managers.Entity.All.Values)
      entity.Init(this);
    Global.Init(this);
    Managers.Debug.PrintSeparator(ConsoleColor.Yellow, "Initialisation fully ended! Enjoy! :D");
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
    Managers.Scene.Current!.Leave(this);
    Raylib.CloseWindow();
    Raylib.CloseAudioDevice();
  }
}