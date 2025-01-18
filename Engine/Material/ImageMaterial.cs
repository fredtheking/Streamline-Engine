using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

//porno gay porno pidor porno porno 
//easter egg by: Mr_CorteZzik
namespace StreamlineEngine.Engine.Material;

public class ImageMaterial : MaterialTemplate<string?, Texture2D?>
{
  public Vector2 Size { get; protected set; }
  public Shader? Shader { get; protected set; } = null;
  public TextureFilter? Filter { get; protected set; } = TextureFilter.Point;

  public ImageMaterial(string filename)
  {
    Filename = Config.ResourcesPath + filename;
  }

  public ImageMaterial(Image image)
  {
    Material = Raylib.LoadTextureFromImage(image);
    Size = new Vector2(image.Width, image.Height);
  }
  
  public ImageMaterial(Texture2D texture)
  {
    Material = texture;
    Size = new Vector2(texture.Width, texture.Height);
  }

  public void AddFilter(TextureFilter filter) => Filter = filter;

  public override void Init(Context context)
  {
    Texture2D texture = Raylib.LoadTexture(Filename);
    Size = new Vector2(texture.Width, texture.Height);
    Raylib.UnloadTexture(texture);
  }

  public override bool Ready() => Material is not null && Raylib.IsTextureValid((Texture2D)Material!);

  public override void Enter(Context context)
  {
    if (Filename is null || Ready()) return;
    Material = Raylib.LoadTexture(Filename);
    if (Filter is not null) Raylib.SetTextureFilter((Texture2D)Material, (TextureFilter)Filter); 
  }

  public override void Leave(Context context)
  {
    if (Filename is null || !Ready()) return;
    Raylib.UnloadTexture((Texture2D)Material!);
    Material = null;
  }
}

