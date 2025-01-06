using System.Numerics;
using Raylib_cs;

namespace StreamlineEngine;

public enum FigureType
{
  Rectangle,
  Rounded,
  Circle
}

public struct Defaults
{
  public static float Unit = 42;
  public static Vector2 Position = new(Config.WindowSize.X / 2, Config.WindowSize.Y / 2);
  public static Vector2 Size = new(Unit);
  public static FigureType Figure = FigureType.Rectangle;
  public static int RoundedSegments = 100;
  public static int ShortUuidLength = 3;
  
  public static Color DebugHitboxColor = new(255, 0, 0, 75);
  public static Color DebugTextBorderColor = new(0, 255, 0);
  public static Color DebugImageBorderColor = new(255, 0, 255);
  public static Color DebugAnimationBorderColor = new(255, 255, 0);
}