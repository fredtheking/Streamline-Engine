using StreamlineEngine.Engine.Pkg;
using StreamlineEngine.Engine.Pkg.ECS.Component;
using StreamlineEngine.Engine.Pkg.ECS.Entity;

namespace StreamlineEngine;

public static class Registration
{
  public static void Init()
  {
    Entity entity = new("HelloObject",
      new PositionCom());
  }
}