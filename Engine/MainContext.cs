using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Manager;

namespace StreamlineEngine.Engine;

public class MainContext
{
  public Managers Managers { get; } = new();
  public Global Global { get; } = new();
  public static RootFolder Root { get; } = new("root", Config.Scenes);
  public static Config.Defaults Const { get; } = new();
  
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
    Registration.MaterialsInitChanges(this);
    System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(Config.Defaults).TypeHandle);
    Managers.Resource.RegisterFromStruct(this);
    Managers.Item.RegisterFromStruct(this);
    Managers.Folder.RegisterFromStruct(this);
    Managers.Debug.PrintSeparator(ConsoleColor.Yellow);
    Root.Init(this);
    Global.Init(this);
    Managers.Debug.PrintSeparator(ConsoleColor.Green, "Initialisation fully ended! Enjoy! :D");
  }

  private void Loop()
  {
    while (!Raylib.WindowShouldClose())
    {
      Raylib.BeginDrawing();
      Raylib.ClearBackground(Config.WindowBackgroundColor);
      
      if (Root.Changed) MainLoop.Enter(this);
      MainLoop.Update(this);
      MainLoop.Draw(this);
      
      Raylib.EndDrawing();
    }
  }

  private void Close()
  {
    Managers.Debug.PrintSeparator(ConsoleColor.Blue, "Terminating program and unloading resources...");
    Root.Leave(this);
    Raylib.CloseWindow();
    Raylib.CloseAudioDevice();
    Managers.Debug.PrintSeparator(ConsoleColor.Green, "Too-da-loo, kangaroo!");
  }
}