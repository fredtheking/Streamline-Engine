using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

//porno gay porno pidor porno porno 
//easter egg by: Mr_CorteZzik
namespace StreamlineEngine.Engine.Material;

public class ImageMaterial : MaterialTemplate
{
  public Vector2 Size { get; private set; }
  public Shader? Shader { get; private set; } = null;
  private TextureFilter Filter { get; set; } = TextureFilter.Point;

  public ImageMaterial(int resourceId, bool loadOnNeedOnly = false)
  {
    Id = resourceId;
    LoadOnNeed = loadOnNeedOnly;
  }

  public void AddFilter(TextureFilter filter) => Filter = filter;
  public void AddShader(Shader shader) => Shader = shader;

  public override void Init(Context context)
  {
    Texture2D texture = context.Managers.Package.Unpack<Texture2D>(Id);
    Size = new Vector2(texture.Width, texture.Height);
    Raylib.UnloadTexture(texture);
  }

  public override bool Ready() => Material is not null && Raylib.IsTextureValid((Texture2D)Material!);

  public override void Load(Context context)
  {
    Material = context.Managers.Package.Unpack<Texture2D>(Id);
    Raylib.SetTextureFilter((Texture2D)Material, Filter); 
  }

  public override void Unload(Context context)
  {
    Raylib.UnloadTexture(Material);
    Material = null;
  }
}

