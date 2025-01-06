using System.Numerics;
using Raylib_cs;

namespace StreamlineEngine;

public struct Config
{
  public enum Scenes
  {
    TestingOne,
    TestingTwo,
  }
  
  public static Scenes StartScene = Scenes.TestingOne;
  
  public static Vector2 WindowSize = new(600, 600);
  public static Color WindowBackgroundColor = Color.Gray;
  public static string WindowTitle = "Streamline Engine";
  public static ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow | ConfigFlags.HighDpiWindow;
  
  public static bool DebugMode = true;
}