using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Manager;
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
    public static readonly Dictionary<string, string> ResourcesPackDictionary = PackageManager.GetJsonToPackAsDict("ResourcesDict.json");
  #endif
  
  public const string ResourcesPackageName = "resources";
  public const string ResourcesPath = "Resources/";
  public static readonly Vector2 WindowSize = new(1280, 720);
  public static readonly Color WindowBackgroundColor = Color.Black;
  public const string WindowTitleInit = "Streamline Engine";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow | ConfigFlags.HighDpiWindow;

  public const bool DebugMode = true;
}