using Raylib_cs;
using StreamlineEngine.Engine.Pkg.ECS.Component.Links;
using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.ECS.Component;

public enum HitboxType
{
  Rectangle,
  Rounded,
  Circle
}

public class HitboxCom : RectangleLink, IScript
{
  public HitboxType Type { get; set; }
  public Color Color { get; set; }
  
  public HitboxCom(PositionCom posCom, SizeCom sizeCom, HitboxType type) { Position = posCom; Size = sizeCom; Type = type; Color = Config.DebugHitboxColor; }
  public HitboxCom(PositionCom posCom, SizeCom sizeCom, HitboxType type, Color debugColor) { Position = posCom; Size = sizeCom; Type = type; Color = debugColor; }
}