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

public class AnimationComponent : ComponentTemplate, ICloneable<AnimationComponent>
{
  public PositionComponent LocalPosition { get; set; }
  public PositionComponent Position { get; set; }
  public SizeComponent LocalSize { get; set; }
  public SizeComponent Size { get; set; }
  public BorderComponent Border { get; set; }
  public ImageCollectionMaterial Resource { get; init; }
  public AnimationChangingType Type { get; set; }
  public SeTimer? Timer { get; private set; }
  private float? ElapsedTime { get; set; }
  public float FrameTime { get; set; }
  public int Index { get; set; }
  /// <summary>
  /// By default, crop is {0, 0, width, height} (full image, no crop)
  /// </summary>
  public Rectangle Crop { get; set; }
  public Color Color { get; set; }
  private bool CropInit { get; set; }
  private bool FrameTimeInit { get; set; }

  public AnimationComponent(ImageCollectionMaterial collection, AnimationChangingType type, Color? color = null)
  {
    Resource = collection;
    Type = type;
    FrameTimeInit = true;
    CropInit = true;
    Color = color ?? Color.White;
    DebugBorderColor = Color.Blue;
    PostSetting();
  }
  
  public AnimationComponent(ImageCollectionMaterial collection, AnimationChangingType type, int fps, Color? color = null)
  {
    Resource = collection;
    Type = type;
    FrameTime = 1f / fps;
    CropInit = true;
    Color = color ?? Color.White;
    DebugBorderColor = Color.Blue;
    PostSetting();
  }
  
  public AnimationComponent(ImageCollectionMaterial collection, AnimationChangingType type, Rectangle crop, Color? color = null)
  {
    Resource = collection;
    Type = type;
    FrameTimeInit = true;
    Crop = crop;
    Color = color ?? Color.White;
    DebugBorderColor = Color.Blue;
    PostSetting();
  }
  
  public AnimationComponent(ImageCollectionMaterial collection, AnimationChangingType type, int fps, Rectangle crop, Color? color = null)
  {
    Resource = collection;
    Type = type;
    FrameTime = 1f / fps;
    Crop = crop;
    Color = color ?? Color.White;
    DebugBorderColor = Color.Blue;
    PostSetting();
  }

  private void PostSetting()
  {
    Timer = Type is AnimationChangingType.FrameSynced ? new SeTimer(FrameTime) : null;
    ElapsedTime = Type is AnimationChangingType.DeltaSynced ? 0f : null;
  }
  
  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      if (CropInit) Crop = new Rectangle(Vector2.Zero, Resource.SharedSize);
      if (FrameTimeInit) FrameTime = Defaults.FrameTime;

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
      if (figure is not null && figure.Type is not FigureType.Rectangle) Error(context, "Animation component support only 'Rectangle' figure type! Rendering might look weird.");

      if (Parent.ComponentTry<FillComponent>() is not null) Information(context, "Animation and Fill component are located in the same item. Be careful with declaring them!");

      Parent.AddMaterials(Resource);
      Parent.LocalPosSizeToLateInit(this);
    });
  }

  public override void Enter(Context context)
  {
    if (Type is AnimationChangingType.FrameSynced) Timer!.Activate();
  }

  public override void Leave(Context context)
  {
    if (Type is AnimationChangingType.FrameSynced) Timer!.FactoryReset();
  }

  public override void Update(Context context)
  {
    switch (Type)
    {
      case AnimationChangingType.DeltaSynced:
        ElapsedTime += Raylib.GetFrameTime();
        while (ElapsedTime >= FrameTime)
        {
          Index = (Index + 1) % Resource.Id.Length;
          ElapsedTime -= FrameTime;
        }
        break;
      case AnimationChangingType.FrameSynced:
        Timer!.Update();
        if (Timer.Target()) Index = (Index + 1) % Resource.Id.Length;
        break;
      case AnimationChangingType.Random:
        Index = new Random().Next(Resource.Id.Length);
        break;
    }
  }

  public override void Draw(Context context)
  {
    if (Type == AnimationChangingType.Manual) IndexNormalize();
    Raylib.DrawTexturePro(Resource.Material![Index], Crop, new Rectangle(Position.Vec2  + LocalPosition.Vec2 + new Vector2(Border.Thickness), Size.Vec2  + LocalSize.Vec2 - new Vector2(Border.Thickness*2)), Vector2.Zero, 0, Color);
  }
  
  public AnimationComponent Clone() => (AnimationComponent)MemberwiseClone();

  private void IndexNormalize()
  {
    if (Index > Resource.Id.Length - 1) Index = 0;
    else if (Index < 0) Index = Resource.Id.Length - 1;
  }

  public override void DebuggerInfo(Context context)
  {
    base.DebuggerInfo(context);
    Extra.TransformImGuiInfo(Position, Size, Color, LocalPosition, LocalSize);
    ImGui.Text($"Crop: {Crop}");
    ImGui.Separator();
    Extra.LinkToAnotherObjectImGui(context, "Border", Border);
    Extra.LinkToAnotherObjectImGui(context, "Resource", Resource);
    ImGui.Text($"Index: {Index}");
    ImGui.Text($"FPS/Frame Time: {1f / FrameTime}/{FrameTime}");
    ImGui.Text($"Animation Changing Type: {Type}");
  }
}