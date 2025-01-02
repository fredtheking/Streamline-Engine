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
    entity.AddComponent(new ObjectComponent(
      new PositionComponent(),
      new SizeComponent(150, 100),
      Color.White
    ));
    entity.AddComponent(new FigureComponent(FigureType.Circle));
  }
}