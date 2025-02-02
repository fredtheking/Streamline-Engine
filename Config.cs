using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Node;

namespace StreamlineEngine;

public struct Config
{
  public static readonly Folder[] RootFolders =
  [
    #if !RESOURCES
    Registration.Folders.FirstScene,
    Registration.Folders.SecondScene,
    Registration.Folders.GlobalFolder
    #endif
  ];
  
  #if !RESOURCES
    public static readonly Folder StartScene = Registration.Folders.FirstScene;
  #else
    public static readonly Dictionary<string, string> ResourcesPackDictionary = Registration.PackResources();
  #endif
  
  public const string ResourcesPackageName = "resources";
  public const string ResourcesPath = "Resources/";
  public static readonly Vector2 WindowSize = new(1920, 1080);
  public static readonly Color WindowBackgroundColor = Color.Black;
  public const string WindowTitleInit = "Streamline Engine";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow | ConfigFlags.HighDpiWindow;

  public const bool DebugMode = true;
}