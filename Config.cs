using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Engine.Manager;
using StreamlineEngine.Engine.Node;

namespace StreamlineEngine;

public struct Config
{
  public static readonly Folder[] RootFolders =
  [
    #if !RESOURCES
    Registration.Folders.GlobalFolder,
    Registration.Folders.FirstScene,
    Registration.Folders.SecondScene,
    Registration.Folders.ThirdScene,
    #endif
  ];
  
  #if !RESOURCES
    public static readonly Folder StartScene = Registration.Folders.SecondScene;
  #endif
  
  public const string ResourcesJsonFilename = "ResourcesDict.json";
  public const string ResourcesPackageName = "resources";
  public const string ResourcesPath = "Resources/";
  
  public const int FpsLock = -1;
  public static readonly Vector2 WindowSize = new(1280, 720);
  public static readonly Color WindowBackgroundColor = Color.Black;
  public const string WindowTitleInit = "Streamline Engine";
  public const string Version = "PRE.WIP";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow | ConfigFlags.HighDpiWindow;

  public const bool DebugModeByDefault = true;
}