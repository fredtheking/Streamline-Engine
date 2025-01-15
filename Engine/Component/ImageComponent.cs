using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;
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

  public ImageComponent(ImageMaterial material)
  {
    Resource = material;
    Crop = new Rectangle(Vector2.Zero, Resource.Size);
  }
  
  public ImageComponent(ImageMaterial material, Rectangle crop)
  {
    Resource = material;
    Crop = crop;
  }
  
  public override void Init(MainContext context)
  {
    StaticEntity staticEntity = context.Managers.Entity.GetByComponent(this);
    Position = staticEntity.Component<PositionComponent>() ?? Error(new PositionComponent(), "Entity has no position component. Initialising default position.");
    Size = staticEntity.Component<SizeComponent>() ?? Error(new SizeComponent(), "Entity has no size component. Initialising default size.");
    Border = staticEntity.Component<BorderComponent>() ?? new BorderComponent(0);
    
    if (staticEntity.Component<FigureComponent>()?.Type is not FigureType.Rectangle) Critical("Image component support only 'Rectangle' figure type! Image rendering might look weird.");
    staticEntity.AddMaterial(Resource);

    FillComponent? filler = staticEntity.Component<FillComponent>();
    if (filler is not null) Warning("Image and Fill component are located in the same entity. Be careful with declaring them!");
    
    staticEntity.LocalLateInit(this);
  }

  public override void Draw(MainContext context)
  {
    Raylib.DrawTexturePro((Texture2D)Resource.Material!, Crop, new Rectangle(Position.Vec2  + LocalPosition.Vec2 + new Vector2(Border.Thickness), Size.Vec2  + LocalSize.Vec2 - new Vector2(Border.Thickness*2)), Vector2.Zero, 0, Color.White);
  }
  
  public ImageComponent Clone() => (ImageComponent)MemberwiseClone();
}