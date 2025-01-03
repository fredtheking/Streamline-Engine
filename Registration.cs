using Raylib_cs;
using StreamlineEngine.Engine.Pkg;
using StreamlineEngine.Engine.Pkg.ECS.ComponentDir;
using StreamlineEngine.Engine.Pkg.ECS.EntityDir;
using StreamlineEngine.Engine.Pkg.Etc;

namespace StreamlineEngine;

public static class Registration
{
  public static void Init(GameContext context)
  {
    Entity entity = new(context, "HelloObject", Config.Scenes.TestingOne);
    entity.AddComponent(new PositionComponent());
    entity.AddComponent(new SizeComponent());
    entity.AddComponent(new FillComponent(Color.White));
    entity.AddComponent(new FigureComponent(FigureType.Rounded, .3f));
    entity.AddComponent(new BorderComponent(3f, Color.Red));
  }
}