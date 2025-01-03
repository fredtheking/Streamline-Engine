using System.Numerics;

namespace StreamlineEngine.Engine.Pkg.Etc;

public enum FigureType
{
  Rectangle,
  Rounded,
  Circle
}

public struct DefaultValues
{
  public static Vector2 Position = new(Config.WindowSize.X / 2, Config.WindowSize.Y / 2);
  public static Vector2 Size = new(100);
}