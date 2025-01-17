using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.FolderItem;

namespace StreamlineEngine;

public struct Config
{
  public static readonly Folder[] Scenes =
  [
    Registration.Folders.FirstScene,
  ];
  
  public static readonly Folder StartScene = Registration.Folders.FirstScene;
  
  public const string ResourcesPath = "Resources/";
  public static readonly Vector2 WindowSize = new(800, 800);
  public static readonly Color WindowBackgroundColor = Color.Black;
  public const string WindowTitle = "Streamline Engine";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow | ConfigFlags.HighDpiWindow;
  
  public const bool DebugMode = true;
  
  public struct Defaults
  {
    public float Unit { get; private set; }
    public Vector2 Position { get; private set; }
    public Vector2 Size { get; private set; }
    
    public FigureType Figure { get; private set; }
    public int RoundedSegments { get; private set; }
    public int ShortUuidLength { get; private set; }

    public Color DebugHitboxColor { get; private set; }
    public Color DebugTextBorderColor { get; private set; }
    public Color DebugImageBorderColor { get; private set; }
    public Color DebugAnimationBorderColor { get; private set; }

    public Defaults()
    {
      Unit = 42;
      Position = new(WindowSize.X / 2, WindowSize.Y / 2);
      Size = new(Unit);
      
      Figure = FigureType.Rectangle;
      RoundedSegments = 100;
      ShortUuidLength = 3;
      
      DebugHitboxColor = new(255, 0, 0, 75);
      DebugTextBorderColor = new(0, 255, 0);
      DebugImageBorderColor = new(255, 0, 255);
      DebugAnimationBorderColor = new(255, 255, 0);
    }
  }
}