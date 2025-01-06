using Raylib_cs;
using StreamlineEngine.Engine.Pkg;
using StreamlineEngine.Engine.Pkg.ECS.ComponentDir;
using StreamlineEngine.Engine.Pkg.ECS.EntityDir;
using StreamlineEngine.Engine.Pkg.Etc;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine;

public static class Registration
{
  public static void EntitiesInit(GameContext context)
  {
    Entity entity = new(context, "HelloObject", Config.Scenes.TestingOne);
    entity.AddComponent(new FigureComponent(FigureType.Circle, .3f));
    entity.AddComponent(new PositionComponent());
    entity.AddComponent(new SizeComponent(100, 120));
    entity.AddComponent(new FillComponent());
    entity.AddComponent(new BorderComponent(4f));
    entity.AddComponent(new MouseHitboxComponent());
  }
}