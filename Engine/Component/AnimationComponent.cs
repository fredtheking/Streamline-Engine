using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Material;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Component;

public class AnimationComponent : ComponentTemplate, ICloneable<AnimationComponent>
{
  public PositionComponent LocalPosition { get; set; }
  public PositionComponent Position { get; set; }
  public SizeComponent LocalSize { get; set; }
  public SizeComponent Size { get; set; }
  public BorderComponent Border { get; set; }
  public ImageCollectionMaterial Resource { get; set; }
  public AnimationChangingType Type { get; set; }
  public int Index { get; set; }
  /// <summary>
  /// By default, crop is {0, 0, width, height} (full image, no crop)
  /// </summary>
  public Rectangle Crop { get; set; }
  private bool CropInit { get; set; }

  public AnimationComponent(ImageCollectionMaterial collection, AnimationChangingType type)
  {
    Resource = collection;
    Type = type;
    CropInit = true;
  }
  
  public AnimationComponent(ImageCollectionMaterial collection, AnimationChangingType type, Rectangle crop)
  {
    Resource = collection;
    Type = type;
    Crop = crop;
  }
  
  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      if (CropInit) Crop = new Rectangle(Vector2.Zero, Resource.SharedSize);
      Item item = context.Managers.Object.GetByComponent(this);
      Position = item.ComponentTry<PositionComponent>() ?? Error(context, new PositionComponent(), "Item has no position component. Initialising default position.");
      Size = item.ComponentTry<SizeComponent>() ?? Error(context, new SizeComponent(), "Item has no size component. Initialising default size.");
      Border = item.ComponentTry<BorderComponent>() ?? new BorderComponent(0);
    
      if (item.ComponentTry<FigureComponent>()?.Type is not FigureType.Rectangle) Error(context, "Image component support only 'Rectangle' figure type! Image rendering might look weird.");
      item.AddMaterials(Resource);
      
      if (item.ComponentTry<FillComponent>() is not null) Information(context, "Image and Fill component are located in the same item. Be careful with declaring them!");
    
      item.LocalLatePosSizeInit(this);
    });
  }

  public override void Update(Context context)
  {
    switch (Type)
    {
      case AnimationChangingType.Delta:
        break;
      case AnimationChangingType.Frame:
        break;
      case AnimationChangingType.Random:
        Index = new Random().Next(Resource.Id.Length);
        break;
    }
  }

  public override void Draw(Context context)
  {
    if (Type == AnimationChangingType.Selectable) IndexNormalize();
    Raylib.DrawTexturePro(Resource.Material![Index], Crop, new Rectangle(Position.Vec2  + LocalPosition.Vec2 + new Vector2(Border.Thickness), Size.Vec2  + LocalSize.Vec2 - new Vector2(Border.Thickness*2)), Vector2.Zero, 0, Color.White);
  }
  
  public AnimationComponent Clone() => (AnimationComponent)MemberwiseClone();

  private void IndexNormalize()
  {
    if (Index > Resource.Id.Length - 1) Index = 0;
    else if (Index < 0) Index = Resource.Id.Length - 1;
  }
}