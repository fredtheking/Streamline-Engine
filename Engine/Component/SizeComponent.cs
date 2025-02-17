using System.Numerics;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Component;

public class SizeComponent : ComponentTemplate, ICloneable<SizeComponent>
{
  public float Width { get; set; }
  public float Height { get; set; }
  public Vector2 Vec2 => new(Width, Height); 
  private bool InitWait;

  public SizeComponent() { InitWait = true; }
  public SizeComponent(Vector2 size) { Width = size.X / 2; Height = size.Y / 2; }
  public SizeComponent(float w, float h) { Width = w; Height = h; }
  public SizeComponent(float wh) { Width = wh; Height = wh; }
  
  public void Set(Vector2 size) { Width = size.X; Height = size.Y; }
  public void Set(float w, float h) { Width = w; Height = h; }
  public void Set(float wh) { Width = wh; Height = wh; }
  public void Add(Vector2 size) { Width += size.X; Height += size.Y; }
  public void Add(float w, float h) { Width += w; Height += h; }
  public void Add(float wh) { Width += wh; Height += wh; }

  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      if (!InitWait) return;
      Item item = context.Managers.Object.GetByComponent(this);

      ImageComponent? image = item.ComponentTry<ImageComponent>();
      TextComponent? text = item.ComponentTry<TextComponent>();
      if (image is not null)
      {
        Information(context, "Found image component! Using image size.", true);
        Width = image.Resource.Size.X;
        Height = image.Resource.Size.Y;
        return;
      }
      else if (text is not null && text.Settings.Autosize)
      {
        Information(context, "Found text component! Using text length.", true);
        if (text.Settings.Wrap) Warning(context, "Its better to enter a size if wrapping in TextComponent is turned on.");
        Vector2 sizo = text.MeasureText(context, text.Text.Value);
        Width = sizo.X;
        Height = sizo.Y;
      }
      else if (image is null && text is null || !text.Settings.Autosize)
      {
        Width = Defaults.Size.X;
        Height = Defaults.Size.Y;
      }
    });
  }
  
  public SizeComponent Clone() => (SizeComponent)MemberwiseClone();
}