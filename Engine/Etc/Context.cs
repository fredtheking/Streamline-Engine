using Raylib_cs;
using StreamlineEngine.Engine.Manager;
using StreamlineEngine.Engine.Node;

namespace StreamlineEngine.Engine.Etc;

public class Context
{
  public Managers Managers { get; } = new();
  public Global Global { get; } = new();
  public Root Root { get; } = new(Config.RootFolders);
  
  public void Run()
  {
    #if RESOURCES
      Managers.Package.Pack(Config.ResourcesPackDictionary);
    #else
      Init();
      Loop();
      Close();
    #endif
  }
  
  private void Init()
  {
    Raylib.SetConfigFlags(Config.WindowConfigFlags);
    Raylib.InitWindow((int)Config.WindowSize.X, (int)Config.WindowSize.Y, Config.WindowTitleInit);
    Raylib.InitAudioDevice();
    Managers.Debug.Separator(ConsoleColor.Yellow, "Window and audio created. Starting importing game assets...");
    Registration.MaterialsInitChanges(this);
    Registration.ItemsInitChanges(this);
    Registration.FoldersInitChanges(this);
    Managers.Resource.RegisterFromStruct(this);
    Managers.Item.RegisterFromStruct();
    Managers.Folder.RegisterFromStruct();
    Managers.Debug.Separator(ConsoleColor.Yellow, "Structs import done! Starting root initialisation...");
    Looper.Init(this);
    Managers.Debug.Separator(ConsoleColor.Green, "Initialisation fully ended! Enjoy! :D");
  }

  private void Loop()
  {
    while (!Raylib.WindowShouldClose())
    {
      Raylib.BeginDrawing();
      Raylib.ClearBackground(Config.WindowBackgroundColor);
      
      Looper.Update(this);
      Looper.Draw(this);
      
      Raylib.EndDrawing();
    }
  }

  private void Close()
  {
    Managers.Debug.Separator(ConsoleColor.Blue, "Terminating program and unloading resources...");
    Root.Leave(this);
    Raylib.CloseWindow();
    Raylib.CloseAudioDevice();
    Managers.Debug.Separator(ConsoleColor.Green, "Too-da-loo, kangaroo!");
  }
}