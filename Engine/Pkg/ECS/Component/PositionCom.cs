using System.Numerics;
using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.ECS.Component;

public class PositionCom : ComponentGroup, IScript
{
  public int X { get; set; }
  public int Y { get; set; }
  
  public PositionCom() { X = (int)(Config.WindowSize.X / 2); Y = (int)(Config.WindowSize.Y / 2); }
  public PositionCom(Vector2 pos) { X = (int)(pos.X / 2); Y = (int)(pos.Y / 2); }
  public PositionCom(int x, int y) { X = x; Y = y; }
  public PositionCom(int xy) { X = xy; Y = xy; }
  public PositionCom(float x, float y) { X = (int)x; Y = (int)y; }
  public PositionCom(float xy) { X = (int)xy; Y = (int)xy; }
}