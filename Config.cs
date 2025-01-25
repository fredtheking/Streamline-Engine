using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Folder;

namespace StreamlineEngine;

public struct Config
{
  public static readonly FolderNode[] RootFolders =
  [
    #if !RESOURCES
    Registration.Folders.FirstScene,
    Registration.Folders.SecondScene,
    Registration.Folders.GlobalNode
    #endif
  ];
  
  #if !RESOURCES
  public static readonly FolderNode StartScene =
    Registration.Folders.FirstScene;
  #endif
  
  public const string ResourcesPackageName = "resources";
  public static readonly Dictionary<string, string> ResourcesPackDictionary = Registration.PackResources();
  public const string ResourcesPath = "Resources/";
  public static readonly Vector2 WindowSize = new(1920, 1080);
  public static readonly Color WindowBackgroundColor = Color.Black;
  public const string WindowTitleInit = "Streamline Engine";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow | ConfigFlags.HighDpiWindow;

  public const bool DebugMode = true;
}