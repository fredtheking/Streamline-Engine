using System.Numerics;
using Raylib_cs;
using StreamlineEngine.CustomBehavior;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Builders;
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
  public class Materials
  {
    public static readonly SoundMaterial VenjentTestSound = new(ResourcesIDs.VenjentSound);
    public static readonly SoundMaterial SetSound = new(ResourcesIDs.SetSound);
    public static readonly FontMaterial FontConsolas = new(ResourcesIDs.Consolas);
    public static readonly ImageMaterial ImageMaterial = new(ResourcesIDs.Bg);
    public static readonly ImageMaterial AvatarMaterial = new(ResourcesIDs.Lion);
    public static readonly ImageCollectionMaterial Collection = new(ResourcesIDs.Jumpscare);
    public static readonly ImageCollectionMaterial MethoidCollection = ImageCollectionMaterial.FromImageMaterial(AvatarMaterial, 3);
  }
  
  public class Items
  {
    public static readonly Item Item = new("HelloItem", ItemObjectType.Dynamic,
      new SizeComponent(200),
      new PositionComponent(),
      new FigureComponent(FigureType.Rounded, .2f),
      new FillComponent(Color.White),
      new BorderComponent(4f, Color.Red),
      new MouseHitboxComponent(),
      new ScriptComponent(new PressChange()),
      new SoundComponent(Materials.VenjentTestSound, stopOnLeave: false)
    );

    public static readonly Item Item2 = new("Hello2Item", ItemObjectType.Dynamic,
      new SizeComponent(200),
      new PositionComponent(),
      new LayerComponent(-1),
      new ScriptComponent(new BonnieScript()),
      new AnimationComponent(Materials.Collection, AnimationChangingType.Delta, 19),
      new BorderComponent(4f, Color.Blue)
    );

    public static readonly Item Item2Helper = new("Hello2Helper", ItemObjectType.Dynamic,
      new SizeComponent(200),
      new PositionComponent(),
      new FigureComponent(FigureType.Rectangle, .2f),
      new AnimationComponent(Materials.MethoidCollection, AnimationChangingType.Random),
      new BorderComponent(4f, Color.Orange)
    );
    
    public static readonly Item Item4 = new("GlobalThingo2", ItemObjectType.Static,
      new SizeComponent(100),
      new ImageComponent(Materials.AvatarMaterial)
    );
    
    public static readonly Item Item3 = new("GlobalThingo", ItemObjectType.Dynamic,
      new SizeComponent(100),
      new PositionComponent(50),
      new ImageComponent(Materials.ImageMaterial),
      new MouseHitboxComponent(),
      new ScriptComponent(new AddPositionOnClick()),
      new SoundComponent(Materials.SetSound, overrideSound: false)
    );

    public static readonly Item ItemTextTest = new("TextTest", ItemObjectType.Dynamic,
      new SizeComponent(1280, 70),
      new PositionComponent(0, 0),
      //new ScriptComponent(new TextResizingToMouse()),
      new TextComponent("TestScene", Materials.FontConsolas, 20, Color.White, 
        new TextSettings.Builder()
          .SetAlignAxisX(TextSettings.TextAlign.Center)
          .SetAlignAxisY(TextSettings.TextAlign.Center)
          .SetBetweenLetterSpacing(-1)
          .SetLineSpacing(-2)
          .Build()
      )
    );
  }
  
  public class Folders
  {
    public static readonly Folder Item = new("HelloFolder", FolderNodeType.Item, Items.Item);
    public static readonly Folder Item2 = new("Hello2Folder", FolderNodeType.Item, Items.Item2, Items.Item2Helper, Items.ItemTextTest);
    
    public static readonly Folder GlobalFolder = new("GlobalNode", FolderNodeType.Item, Items.Item3, Items.Item4);
    public static readonly Folder FirstScene = new("FirstScene", FolderNodeType.Scene, Item);
    public static readonly Folder SecondScene = new("SecondScene", FolderNodeType.Scene, Item2);
    public static readonly Folder ThirdScene = new("ThirdScene", FolderNodeType.Scene);
  }
  
  public static void MaterialsInitChanges(Context context)
  {
    Materials.ImageMaterial.AddFilter(TextureFilter.Bilinear);
  }
  
  public static void ItemsInitChanges(Context context)
  {
    Items.Item4.AddEarlyInit(InitType.Item, obj => obj.AddComponents(
      Items.Item3.Component<PositionComponent>()
      ));
    Items.Item4.AddLateInit(InitType.Item, obj => obj.Component<ImageComponent>().LocalPosition.Add(new Vector2(130, 0)));
    Items.Item.AddLateInit(InitType.Item, obj => obj.Component<PositionComponent>().Add(-70));
    Items.Item2.AddLateInit(InitType.Item, obj => obj.Component<PositionComponent>().Add(-300, 0));
  }
  
  public static void FoldersInitChanges(Context context)
  {
    
  }
}
#endif