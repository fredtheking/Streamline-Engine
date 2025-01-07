using StreamlineEngine.Engine;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Pkg;

namespace StreamlineEngine;

public static class Registration
{
  public static void EntitiesCreation(GameContext context)
  {
    StaticEntity staticEntity = new(context, "HelloObject", Config.Scenes.TestingOne);
    staticEntity.AddComponent(new FigureComponent(FigureType.Rounded, .6f));
    staticEntity.AddComponent(new PositionComponent());
    staticEntity.AddComponent(new SizeComponent(100, 120));
    staticEntity.AddComponent(new FillComponent());
    staticEntity.AddComponent(new BorderComponent(4f));
    staticEntity.AddComponent(new MouseHitboxComponent());
  }

  public static void MaterialsCreation(GameContext context)
  {
    ImageMaterial imageMaterial = new("Image/test.png");
    context.Managers.Entity.GetByName("HelloObject").AddMaterial(imageMaterial);
  }
}