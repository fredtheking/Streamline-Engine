using Raylib_cs;
using StreamlineEngine.Custom;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Folder;
using StreamlineEngine.Engine.FolderItem;
using StreamlineEngine.Engine.Material;
using Color = Raylib_cs.Color;

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
      new SizeComponent(800),
      new PositionComponent(),
      new FigureComponent(FigureType.Rectangle, .2f),
      //new FillComponent(Color.White),
      new ImageComponent(Materials.ImageMaterial),
      new BorderComponent(8f, Color.Blue),
      new BorderComponent(4f, Color.Red),
      new MouseHitboxComponent(),
      new CustomScriptComponent(new PressChange())
    );
  }
  
  public struct Folders
  {
    public static FolderNode FolderItem = new("HelloFolder", FolderNodeType.Item, Items.Item);
    public static FolderNode FolderItem2 = new("Hello2Folder", FolderNodeType.Item);
    
    public static FolderNode FirstScene = new("FirstScene", FolderNodeType.Scene, FolderItem);
    public static FolderNode SecondScene = new("SecondScene", FolderNodeType.Scene, FolderItem2);
  }
  
  public static void MaterialsInitChanges(Context context)
  {
    Materials.ImageMaterial.AddFilter(TextureFilter.Bilinear);
  }
  
  public static void ItemsInitChanges(Context context)
  {
    //Items.Item.AddLateInit(LateInitType.Item, () => Items.Item.Component<FillComponent>()!.LocalPosition.Add(-4));
    //Items.Item.AddLateInit(LateInitType.Item, () => Items.Item.Component<FillComponent>()!.LocalSize.Add(8));
  }
  
  public static void FoldersInitChanges(Context context)
  {
    
  }
}