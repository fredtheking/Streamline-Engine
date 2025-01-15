using Raylib_cs;
using StreamlineEngine.Engine;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.EntityMaterial;

namespace StreamlineEngine;

public static class Registration
{
  public struct Entities
  {
    public static StaticEntity staticEntity = new("HelloObject", [Config.Scenes.TestingOne], [
      new PositionComponent(),
      new SizeComponent(),
      new FigureComponent(FigureType.Rectangle, .2f),
      new ImageComponent(Materials.imageMaterial),
      new BorderComponent(4f, Color.Red)
    ]);
  }
  
  public struct Materials
  {
    public static ImageMaterial imageMaterial = new("Image/test.png");
  }
  
  public static void EntitiesInitChanges(MainContext context)
  {
    Entities.staticEntity.AddLateInit(LateInitType.Entity, () => Entities.staticEntity.Component<BorderComponent>()!.LocalPosition.Add(30));
  }

  public static void MaterialsInitChanges(MainContext context)
  {
    
  }
}