using Raylib_cs;
using StreamlineEngine.Custom;
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

public static class Registration
{
  public struct Materials
  {
    #if !RESOURCES
    public static ImageMaterial ImageMaterial = new((int)ResourcesIDs.Lion);
    public static ImageMaterial AvatarMaterial = new((int)ResourcesIDs.Avatar);
    #endif
  }
  
  public struct Items
  {
    #if !RESOURCES
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
      new BorderComponent(4f, Color.Blue)
    );

    public static Item Item3 = new("GlobalThingo",
      new SizeComponent(100),
      new PositionComponent(100),
      new FigureComponent(FigureType.Rectangle),
      new ImageComponent(Materials.ImageMaterial)
    );
    #endif
  }
  
  public struct Folders
  {
    #if !RESOURCES
    public static Folder Item = new("HelloFolder", FolderNodeType.Item, Items.Item);
    public static Folder Item2 = new("Hello2Folder", FolderNodeType.Item, Items.Item2);
    
    public static Folder GlobalFolder = new("GlobalNode", FolderNodeType.Item, Items.Item3);
    public static Folder FirstScene = new("FirstScene", FolderNodeType.Scene, Item);
    public static Folder SecondScene = new("SecondScene", FolderNodeType.Scene, Item2);
    #endif
  }
  
  public static void MaterialsInitChanges(Context context)
  {
    #if !RESOURCES
    Materials.ImageMaterial.AddFilter(TextureFilter.Bilinear);
    #endif
  }
  
  public static void ItemsInitChanges(Context context)
  {
    #if !RESOURCES
    Items.Item.AddLateInit(InitType.Item, obj => obj.Component<PositionComponent>()!.Add(-70));
    Items.Item2.AddLateInit(InitType.Item, obj => obj.Component<ImageComponent>()!.Position.Add(40));
    #endif
  }
  
  public static void FoldersInitChanges(Context context)
  {
    #if !RESOURCES
    #endif
  }

  public static Dictionary<string, string> PackResources()
  {
    Dictionary<string, string> resources = [];
    
    resources.Add("Test", "Image/test.png");
    resources.Add("Avatar", "Image/avatar.png");
    resources.Add("Bg", "Image/bg.png");
    resources.Add("Lion", "Image/lion.jpg");

    return resources;
  }
}