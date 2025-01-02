using System.Numerics;
using Raylib_cs;

namespace StreamlineEngine;

public struct Config()
{
  public enum Scenes
  {
    TestingOne,
    TestingTwo,
  }
  
  public static Scenes StartScene = Scenes.TestingOne;
  
  public static Vector2 WindowSize = new(600, 600);
  public static string WindowTitle = "Streamline Engine";
  public static ConfigFlags WindowConfigFlags = ConfigFlags.AlwaysRunWindow | ConfigFlags.HighDpiWindow;
  
  public static int RoundedSegments = 100;

  public static Color DebugHitboxColor = new(255, 0, 0, 75);
  public static Color DebugTextBorderColor = new(0, 255, 0);
  public static Color DebugImageBorderColor = new(255, 0, 255);
  public static Color DebugAnimationBorderColor = new(255, 255, 0);
}