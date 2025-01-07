using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Pkg.Etc.Templates;

namespace StreamlineEngine.Engine.EntityMaterial;

public class ImageMaterial : MaterialTemplate<string?, Texture2D?>
{
  public Vector2 Size { get; protected set; }
  public Shader? Shader { get; protected set; }

  public ImageMaterial(string filename)
  {
    Filename = Config.ResourcesPath + filename;

    Texture2D texture = Raylib.LoadTexture(Filename);
    Size = new Vector2(texture.Width, texture.Height);
    Raylib.UnloadTexture(texture);
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

  public override bool Ready() => Material is not null && Raylib.IsTextureValid((Texture2D)Material!);

  public override void Enter(GameContext context)
  {
    if (Filename is null || Ready()) return;
    Material = Raylib.LoadTexture(Filename);
  }

  public override void Leave(GameContext context)
  {
    if (Filename is null || !Ready()) return;
    Raylib.UnloadTexture((Texture2D)Material!);
    Material = null;
  }
}