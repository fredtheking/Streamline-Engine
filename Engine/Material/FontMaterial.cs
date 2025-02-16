using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Material;

public class FontMaterial : MaterialTemplate
{
  public float Size { get; private set; }
  private TextureFilter Filter { get; set; } = TextureFilter.Point;

  public FontMaterial(int resourceId, bool loadOnNeedOnly = false)
  {
    Id = resourceId;
    LoadOnNeed = loadOnNeedOnly;
  }

  public void AddFilter(TextureFilter filter) => Filter = filter;

  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      Font font = context.Managers.Package.Unpack<Font>(Id);
      Size = font.BaseSize;
      Raylib.UnloadFont(font);
    });
  }

  public override bool Ready() => Material is not null && Raylib.IsFontValid((Font)Material);

  public override void Load(Context context)
  {
    Material = context.Managers.Package.Unpack<Font>(Id);
    Raylib.SetTextureFilter(Material.Texture, Filter);
  }

  public override void Unload(Context context)
  {
    Raylib.UnloadFont(Material);
    Material = null;
  }
}