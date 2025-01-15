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
    Registration.EntitiesInitChanges(this);
    Managers.Entity.RegisterFromStruct(this);
    Registration.MaterialsInitChanges(this);
    Managers.Debug.PrintSeparator(ConsoleColor.Yellow);
    foreach (StaticEntity entity in Managers.Entity.All.Values)
      entity.Init(this);
    Global.Init(this);
    Managers.Debug.PrintSeparator(ConsoleColor.Green, "Initialisation fully ended! Enjoy! :D");
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
    Managers.Debug.PrintSeparator(ConsoleColor.Blue, "Terminating program and unloading resources...");
    Managers.Scene.Current!.Leave(this);
    Raylib.CloseWindow();
    Raylib.CloseAudioDevice();
    Managers.Debug.PrintSeparator(ConsoleColor.Green, "Too-da-loo, kangaroo!");
  }
}