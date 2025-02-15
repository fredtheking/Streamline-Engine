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
    public static readonly Folder StartScene = Registration.Folders.SecondScene;
  #endif
  
  public const string ResourcesJsonFilename = "ResourcesDict.json";
  public const string ResourcesPackageName = "resources";
  public const string ResourcesPath = "Resources/";
  
  public const int FpsLock = 5;
  public static readonly Vector2 WindowSize = new(1920, 1080);
  public static readonly Color WindowBackgroundColor = Color.Black;
  public const string WindowTitleInit = "Streamline Engine";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow | ConfigFlags.HighDpiWindow;

  public const bool DebugMode = true;
  public static readonly string[] PostInitPhrases = [
    "Enjoy! :D",
    "Good to see you there!",
    "All systems go! Time to roll.",
    "Ready for action!",
    "Code’s alive, time to thrive!",
    "Mission start!"
  ];
  public static readonly string[] ByePhrases = [
    "Too-da-loo, kangaroo!",
    "See you later, alligator!",
    "Gotta dash, moustache!",
    "Take care, polar bear!",
    "See you soon, raccoon!",
    "Bye-bye, butterfly!",
    "Catch you later, navigator!",
    "Hasta mañana, iguana!"
  ];
}