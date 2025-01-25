using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Folder;
using StreamlineEngine.Engine.FolderItem;

namespace StreamlineEngine;

public struct Config
{
  public static readonly FolderNode[] RootFolders =
  [
    Registration.Folders.FirstScene,
    Registration.Folders.SecondScene,
    Registration.Folders.GlobalNode
  ];

  public static readonly FolderNode StartScene = Registration.Folders.SecondScene;
  
  public const string ResourcesPackageName = "resources";
  public static readonly Dictionary<string, string> ResourcesPackDictionary = Registration.PackResources();
  public const string ResourcesPath = "Resources/";
  public static readonly Vector2 WindowSize = new(1920, 1080);
  public static readonly Color WindowBackgroundColor = Color.Black;
  public const string WindowTitleInit = "Streamline Engine";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow | ConfigFlags.HighDpiWindow;

  public const bool DebugMode = true;
}