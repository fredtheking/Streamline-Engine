using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Material;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class ImageComponent : ComponentTemplate, ICloneable<ImageComponent>
{
  public PositionComponent LocalPosition { get; set; }
  public PositionComponent Position { get; set; }
  public SizeComponent LocalSize { get; set; }
  public SizeComponent Size { get; set; }
  public BorderComponent Border { get; set; }
  public ImageMaterial Resource { get; set; }
  public Rectangle Crop { get; set; }
  private bool CropInit { get; set; }

  public ImageComponent(ImageMaterial material)
  {
    Resource = material;
    CropInit = true;
  }
  
  public ImageComponent(ImageMaterial material, Rectangle crop)
  {
    Resource = material;
    Crop = crop;
  }
  
  public override void Init(Context context)
  {
    if (CropInit) Crop = new Rectangle(Vector2.Zero, Resource.Size);
    Item.Item item = context.Managers.Item.GetByComponent(this);
    Position = item.Component<PositionComponent>() ?? Error(new PositionComponent(), "Item has no position component. Initialising default position.");
    Size = item.Component<SizeComponent>() ?? Error(new SizeComponent(), "Item has no size component. Initialising default size.");
    Border = item.Component<BorderComponent>() ?? new BorderComponent(0);
    
    if (item.Component<FigureComponent>()?.Type is not FigureType.Rectangle) Error("Image component support only 'Rectangle' figure type! Image rendering might look weird.");
    item.AddMaterial(Resource);

    FillComponent? filler = item.Component<FillComponent>();
    if (filler is not null) Information("Image and Fill component are located in the same item. Be careful with declaring them!");
    
    item.LocalLateInit(this);
  }

  public override void Draw(Context context)
  {
    Raylib.DrawTexturePro((Texture2D)Resource.Material!, Crop, new Rectangle(Position.Vec2  + LocalPosition.Vec2 + new Vector2(Border.Thickness), Size.Vec2  + LocalSize.Vec2 - new Vector2(Border.Thickness*2)), Vector2.Zero, 0, Color.White);
  }
  
  public ImageComponent Clone() => (ImageComponent)MemberwiseClone();
}