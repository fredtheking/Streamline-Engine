using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Classes;
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
  public Color Color { get; set; }
  private bool CropInit { get; set; }

  public ImageComponent(ImageMaterial material, Color? color = null)
  {
    Resource = material;
    CropInit = true;
    DebugBorderColor = Color.Yellow;
    Color = color ?? Color.White;
  }
  
  public ImageComponent(ImageMaterial material, Rectangle crop, Color? color = null)
  {
    Resource = material;
    Crop = crop;
    DebugBorderColor = Color.Yellow;
    Color = color ?? Color.White;
  }
  
  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      if (CropInit) Crop = new Rectangle(Vector2.Zero, Resource.Size);

      PositionComponent? position = Parent.ComponentTry<PositionComponent>();
      if (position is null)
      {
        Warning(context, "Item has no position component. Initialising default position.");
        Position = new PositionComponent();
        Parent.AddComponents(Position);
        Position.Init(context);
      }
      else
      {
        Information(context, "Found position component!");
        Position = position;
      }
      
      SizeComponent? size = Parent.ComponentTry<SizeComponent>();
      if (size is null)
      {
        Warning(context, "Item has no size component. Initialising default size.");
        Size = new SizeComponent();
        Parent.AddComponents(Size);
        Size.Init(context);
      }
      else
      {
        Information(context, "Found size component!");
        Size = size;
      }
      
      BorderComponent? border = Parent.ComponentTry<BorderComponent>();
      if (border is null)
      {
        Warning(context, "Item has no border component. Initialising junk border.");
        Border = new BorderComponent{ Junk = true };
      }
      else
      {
        Information(context, "Found border component!");
        Border = border;
      }
      
      FigureComponent? figure = Parent.ComponentTry<FigureComponent>();
      if (figure is not null && figure.Type is not FigureType.Rectangle) Error(context, "Image component support only 'Rectangle' figure type! Rendering might look weird.");
      Parent.AddMaterials(Resource);
      
      if (Parent.ComponentTry<FillComponent>() is not null) Information(context, "Image and Fill component are located in the same item. Be careful with declaring them!");
    
      Parent.LocalPosSizeToLateInit(this);
    });
  }

  public override void Draw(Context context)
  {
    Raylib.DrawTexturePro((Texture2D)Resource.Material!, Crop, new Rectangle(Position.Vec2  + LocalPosition.Vec2 + new Vector2(Border.Thickness), Size.Vec2  + LocalSize.Vec2 - new Vector2(Border.Thickness*2)), Vector2.Zero, 0, Color);
  }
  
  public ImageComponent Clone() => (ImageComponent)MemberwiseClone();

  public override void DebuggerInfo(Context context)
  {
    base.DebuggerInfo(context);
    Extra.TransformImGuiInfo(Position, Size, Color, LocalPosition, LocalSize);
    ImGui.Text($"Crop: {Crop}");
    ImGui.Separator();
    Extra.LinkToProbablyJunkObjectImGui(context, "Border", Border);
    Extra.LinkToAnotherObjectImGui(context, "Resource", Resource);
  }
}