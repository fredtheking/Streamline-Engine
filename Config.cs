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
    #endif
  ];
  
  #if !RESOURCES
    public static readonly Folder StartScene = Registration.Folders.FirstScene;
  #endif
  
  public const string ResourcesJsonFilename = "ResourcesDict.json";
  public const string ResourcesPackageName = "resources";
  public const string ResourcesPath = "Resources/";
  public static readonly Vector2 WindowSize = new(1920, 1080);
  public static readonly Color WindowBackgroundColor = Color.Black;
  public const string WindowTitleInit = "Streamline Engine";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow | ConfigFlags.HighDpiWindow;

  public const bool DebugMode = true;
  public static readonly string[] ByePhrase = [
    "Too-da-loo, kangaroo!",
    "See you later, alligator!"
  ];
}