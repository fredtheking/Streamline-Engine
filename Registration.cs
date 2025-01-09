using Raylib_cs;
using StreamlineEngine.Engine;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Pkg;

namespace StreamlineEngine;

public static class Registration
{
  public struct Entity
  {
    public static StaticEntity staticEntity;
  }
  
  public static void EntitiesCreation(MainContext context)
  {
    Entity.staticEntity = new(context, "HelloObject", Config.Scenes.TestingOne);
    Entity.staticEntity.AddComponent(new FigureComponent(FigureType.Rectangle, .6f));
    Entity.staticEntity.AddComponent(new PositionComponent());
    Entity.staticEntity.AddComponent(new SizeComponent());
    Entity.staticEntity.AddComponent(new BorderComponent(4f, Color.Red));
    Entity.staticEntity.AddComponent(new ImageComponent(new ImageMaterial("Image/test.png")));
  }

  public static void MaterialsCreation(MainContext context)
  {
    
  }
}