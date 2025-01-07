using System.Numerics;
using Raylib_cs;

namespace StreamlineEngine;

public struct Config
{
  public enum Scenes
  {
    TestingOne,
    TestingTwo
  }
  
  public const Scenes StartScene = Scenes.TestingOne;
  
  public const string ResourcesPath = "Resources/";
  public static readonly Vector2 WindowSize = new(600, 600);
  public static readonly Color WindowBackgroundColor = Color.Gray;
  public const string WindowTitle = "Streamline Engine";
  public const ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow | ConfigFlags.HighDpiWindow;
  
  public const bool DebugMode = true;
}