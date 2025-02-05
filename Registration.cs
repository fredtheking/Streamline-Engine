using Raylib_cs;
using StreamlineEngine.CustomBehavior;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Material;
using StreamlineEngine.Engine.Node;
using StreamlineEngine.Engine.Object;
#if !RESOURCES
using StreamlineEngine.Generated;
#endif
using Color = Raylib_cs.Color;

namespace StreamlineEngine;

#if !RESOURCES
public static class Registration
{
  public struct Materials
  {
    public static ImageMaterial ImageMaterial = new((int)ResourcesIDs.Bg);
    public static ImageMaterial AvatarMaterial = new((int)ResourcesIDs.Lion);
  }
  
  public struct Items
  {
    public static Item Item = new("HelloItem",
      new SizeComponent(200),
      new PositionComponent(),
      new FigureComponent(FigureType.Rounded, .2f),
      new FillComponent(Color.White),
      new BorderComponent(4f, Color.Red),
      new MouseHitboxComponent(),
      new ScriptComponent(new PressChange())
    );

    public static Item Item2 = new("Hello2Item",
      new SizeComponent(200),
      new PositionComponent(),
      new FigureComponent(FigureType.Rectangle, .2f),
      new ImageComponent(Materials.AvatarMaterial),
      new BorderComponent(8f, Color.Purple),
      new BorderComponent(4f, Color.Blue)
    );

    public static Item Item3 = new("GlobalThingo",
      new SizeComponent(100),
      new PositionComponent(100),
      new FigureComponent(FigureType.Rectangle),
      new ImageComponent(Materials.ImageMaterial)
    );
  }
  
  public struct Folders
  {
    public static Folder Item = new("HelloFolder", FolderNodeType.Item, Items.Item);
    public static Folder Item2 = new("Hello2Folder", FolderNodeType.Item, Items.Item2);
    
    public static Folder GlobalFolder = new("GlobalNode", FolderNodeType.Item, Items.Item3);
    public static Folder FirstScene = new("FirstScene", FolderNodeType.Scene, Item);
    public static Folder SecondScene = new("SecondScene", FolderNodeType.Scene, Item2);
  }
  
  public static void MaterialsInitChanges(Context context)
  {
    Materials.ImageMaterial.AddFilter(TextureFilter.Bilinear);
  }
  
  public static void ItemsInitChanges(Context context)
  {
    Items.Item.AddLateInit(InitType.Item, obj => obj.Component<PositionComponent>()!.Add(-70));
    //Items.Item2.AddLateInit(InitType.Item, obj => obj.Component<ImageComponent>()!.LocalPosition.Add(40));
  }
  
  public static void FoldersInitChanges(Context context)
  {
    
  }
}
#endif