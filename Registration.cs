using System.Numerics;
using Raylib_cs;
using StreamlineEngine.CustomBehavior;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Material;
using StreamlineEngine.Engine.Node;
using StreamlineEngine.Engine.Object;
#if !RESOURCES
using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Generated;
#endif
using Color = Raylib_cs.Color;

namespace StreamlineEngine;

#if !RESOURCES
public static class Registration
{
  public struct Materials
  {
    public static readonly ImageMaterial ImageMaterial = new(ResourcesIDs.Bg);
    public static readonly ImageMaterial AvatarMaterial = new(ResourcesIDs.Lion);
    public static readonly ImageCollectionMaterial Collection = new(ResourcesIDs.Jumpscare);
    public static readonly ImageCollectionMaterial MethoidCollection = ImageCollectionMaterial.FromImageMaterial(AvatarMaterial, 3);
  }
  
  public struct Items
  {
    public static Item Item = new("HelloItem", ItemObjectType.Dynamic,
      new SizeComponent(200),
      new PositionComponent(),
      new FigureComponent(FigureType.Rounded, .2f),
      new FillComponent(Color.White),
      new BorderComponent(4f, Color.Red),
      new MouseHitboxComponent(),
      new ScriptComponent(new PressChange())
    );

    public static Item Item2 = new("Hello2Item", ItemObjectType.Dynamic,
      new SizeComponent(200),
      new PositionComponent(),
      new LayerComponent(1),
      new ScriptComponent(new BonnieScript()),
      new FigureComponent(FigureType.Rectangle, .2f),
      new AnimationComponent(Materials.Collection, AnimationChangingType.Delta, 19),
      new BorderComponent(4f, Color.Blue)
    );
    
    public static Item Item2Helper = new("Hello2Helper", ItemObjectType.Dynamic,
      new SizeComponent(200),
      new PositionComponent(),
      new FigureComponent(FigureType.Rectangle, .2f),
      new AnimationComponent(Materials.MethoidCollection, AnimationChangingType.Random),
      new BorderComponent(4f, Color.Orange)
    );
    
    public static Item Item4 = new("GlobalThingo2", ItemObjectType.Static,
      new SizeComponent(100),
      new ImageComponent(Materials.AvatarMaterial)
    );
    
    public static Item Item3 = new("GlobalThingo", ItemObjectType.Dynamic,
      new SizeComponent(100),
      new PositionComponent(50),
      new ImageComponent(Materials.ImageMaterial),
      new MouseHitboxComponent(),
       new ScriptComponent(new AddPositionOnClick())
    );
  }
  
  public struct Folders
  {
    public static Folder Item = new("HelloFolder", FolderNodeType.Item, Items.Item);
    public static Folder Item2 = new("Hello2Folder", FolderNodeType.Item, Items.Item2, Items.Item2Helper);
    
    public static Folder GlobalFolder = new("GlobalNode", FolderNodeType.Item, Items.Item3, Items.Item4);
    public static Folder FirstScene = new("FirstScene", FolderNodeType.Scene, Item);
    public static Folder SecondScene = new("SecondScene", FolderNodeType.Scene, Item2);
  }
  
  public static void MaterialsInitChanges(Context context)
  {
    Materials.ImageMaterial.AddFilter(TextureFilter.Bilinear);
  }
  
  public static void ItemsInitChanges(Context context)
  {
    Items.Item4.AddEarlyInit(InitType.Item, obj => obj.AddComponents(
      new PositionComponent(Items.Item3.Component<PositionComponent>().X + 130, Items.Item3.Component<PositionComponent>().Y)
      ));
    Items.Item2Helper.AddLateInit(InitType.Item, obj => obj.Component<PositionComponent>().Set(Items.Item2.Component<PositionComponent>().Vec2 + new Vector2(33)));
    Items.Item.AddLateInit(InitType.Item, obj => obj.Component<PositionComponent>().Add(-70));
  }
  
  public static void FoldersInitChanges(Context context)
  {
    
  }
}
#endif