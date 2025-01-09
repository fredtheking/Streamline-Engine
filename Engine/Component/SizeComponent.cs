using System.Numerics;
using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.Component;

public class SizeComponent : ComponentTemplate
{
  public float Width { get; set; }
  public float Height { get; set; }
  public Vector2 Vec2 => new(Width, Height); 
  private bool InitWait;

  public SizeComponent() { InitWait = true; }
  public SizeComponent(Vector2 size) { Width = size.X / 2; Height = size.Y / 2; }
  public SizeComponent(float w, float h) { Width = w; Height = h; }
  public SizeComponent(float wh) { Width = wh; Height = wh; }

  public override void Init(MainContext context)
  {
    if (!InitWait) return;
    StaticEntity staticEntity = context.Managers.Entity.GetByComponent(this);

    ImageComponent? image = staticEntity.Component<ImageComponent>();
    if (image is not null)
    {
      Information("Found image component! Using image size.", true);
      Width = image.Resource.Size.X;
      Height = image.Resource.Size.Y;
    }
    else
    {
      Error("Entity has no size component. Initialising default size.");
      Width = Defaults.Size.X;
      Height = Defaults.Size.Y;
    }
  }
}