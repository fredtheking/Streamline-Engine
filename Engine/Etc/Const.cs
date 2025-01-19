using System.Numerics;
using Raylib_cs;

namespace StreamlineEngine.Engine.Etc;

public enum LateInitType { Component, Item }
public enum FigureType { Rectangle, Rounded, Circle }
public enum DebuggerType { Floating, Side }
public enum FolderNodeType { Scene, Node, Item }

public struct Defaults
{
  public const float Unit = 42;
  public static readonly Vector2 Position = new(Config.WindowSize.X / 2, Config.WindowSize.Y / 2);
  public static readonly Vector2 Size = new(Unit);

  public const FigureType Figure = FigureType.Rectangle;
  public const int RoundedSegments = 100;
  public const int ShortUuidLength = 3;
  
  public static readonly Color DebugHitboxColor = new(255, 0, 0, 75);
  public static readonly Color DebugTextBorderColor = new(0, 255, 0);
  public static readonly Color DebugImageBorderColor = new(255, 0, 255);
  public static readonly Color DebugAnimationBorderColor = new(255, 255, 0);
}