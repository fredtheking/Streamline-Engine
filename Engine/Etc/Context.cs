using Raylib_cs;
using StreamlineEngine.Engine.Folder;
using StreamlineEngine.Engine.Manager;

namespace StreamlineEngine.Engine.Etc;

public class Context
{
  public Managers Managers { get; } = new();
  public Global Global { get; } = new();
  public FolderRoot Root { get; } = new(Config.RootFolders);
  
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
    Managers.Debug.PrintSeparator(ConsoleColor.Yellow, "Window and audio created. Starting importing game assets...");
    Registration.MaterialsInitChanges(this);
    Registration.ItemsInitChanges(this);
    Registration.FoldersInitChanges(this);
    Managers.Resource.RegisterFromStruct(this);
    Managers.Item.RegisterFromStruct();
    Managers.Folder.RegisterFromStruct();
    Managers.Debug.PrintSeparator(ConsoleColor.Yellow, "Structs import done! Starting root initialisation...");
    Looper.Init(this);
    Managers.Debug.PrintSeparator(ConsoleColor.Green, "Initialisation fully ended! Enjoy! :D");
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
    Managers.Debug.PrintSeparator(ConsoleColor.Blue, "Terminating program and unloading resources...");
    Root.Leave(this);
    Raylib.CloseWindow();
    Raylib.CloseAudioDevice();
    Managers.Debug.PrintSeparator(ConsoleColor.Green, "Too-da-loo, kangaroo!");
  }
}