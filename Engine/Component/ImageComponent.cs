using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Material;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Component;

public class ImageComponent : ComponentTemplate, ICloneable<ImageComponent>
{
  public PositionComponent LocalPosition { get; set; }
  public PositionComponent Position { get; set; }
  public SizeComponent LocalSize { get; set; }
  public SizeComponent Size { get; set; }
  public BorderComponent Border { get; set; }
  public ImageMaterial Resource { get; init; }
  /// <summary>
  /// By default, crop is {0, 0, width, height} (full image, no crop)
  /// </summary>
  public Rectangle Crop { get; set; }
  private bool CropInit { get; set; }

  public ImageComponent(ImageMaterial material)
  {
    Resource = material;
    CropInit = true;
    DebugBorderColor = Color.Yellow;
  }
  
  public ImageComponent(ImageMaterial material, Rectangle crop)
  {
    Resource = material;
    Crop = crop;
    DebugBorderColor = Color.Yellow;
  }
  
  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      if (CropInit) Crop = new Rectangle(Vector2.Zero, Resource.Size);
      
      Position = Parent.ComponentTry<PositionComponent>() ?? Warning(context, new PositionComponent(), "Item has no position component. Initialising default position.");
      Size = Parent.ComponentTry<SizeComponent>() ?? Warning(context, new SizeComponent(), "Item has no size component. Initialising default size.");
      Border = Parent.ComponentTry<BorderComponent>() ?? new BorderComponent(0);
      
      FigureComponent? figure = Parent.ComponentTry<FigureComponent>();
      if (figure is not null && figure.Type is not FigureType.Rectangle) Error(context, "Image component support only 'Rectangle' figure type! Rendering might look weird.");
      Parent.AddMaterials(Resource);
      
      if (Parent.ComponentTry<FillComponent>() is not null) Information(context, "Image and Fill component are located in the same item. Be careful with declaring them!");
    
      Parent.LocalPosSizeToLateInit(this);
    });
  }

  public override void Draw(Context context)
  {
    Raylib.DrawTexturePro((Texture2D)Resource.Material!, Crop, new Rectangle(Position.Vec2  + LocalPosition.Vec2 + new Vector2(Border.Thickness), Size.Vec2  + LocalSize.Vec2 - new Vector2(Border.Thickness*2)), Vector2.Zero, 0, Color.White);
  }
  
  public ImageComponent Clone() => (ImageComponent)MemberwiseClone();
}