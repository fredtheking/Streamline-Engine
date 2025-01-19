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

  public static readonly FolderNode StartScene = Registration.Folders.FirstScene;

  public const string ResourcesPath = "Resources/";
  public static readonly Vector2 WindowSize = new(1920, 1080);
  public static readonly Color WindowBackgroundColor = Color.Black;
  public const string WindowTitle = "Streamline Engine";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow | ConfigFlags.HighDpiWindow;

  public const bool DebugMode = true;
}