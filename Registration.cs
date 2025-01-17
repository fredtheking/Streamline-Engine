using Raylib_cs;
using StreamlineEngine.Custom;
using StreamlineEngine.Engine;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.FolderItem;
using StreamlineEngine.Engine.Material;

namespace StreamlineEngine;

public static class Registration
{
  public struct Materials
  {
    public static ImageMaterial ImageMaterial = new("Image/test.png");
  }
  
  public struct Items
  {
    public static Item Item = new("HelloItem",
      new PositionComponent(),
      new SizeComponent(),
      new FigureComponent(FigureType.Rectangle, .2f),
      new ImageComponent(Materials.ImageMaterial),
      new MouseHitboxComponent(),
      new BorderComponent(4f, Color.Red),
      new CustomScriptComponent(new PressChange())
    );
  }
  
  public struct Folders
  {
    public static Folder Folder = new("HelloFolder", Items.Item);
    
    public static Folder FirstScene = new("FirstScene", Folder);
  }
  
  public static void EntitiesInitChanges(MainContext context)
  {
    Items.Item.AddLateInit(LateInitType.Entity, () => Items.Item.Component<BorderComponent>()!.LocalPosition.Add(0));
  }

  public static void MaterialsInitChanges(MainContext context)
  {
    
  }
}