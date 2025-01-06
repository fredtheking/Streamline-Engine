using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Pkg.Etc.Interfaces;

namespace StreamlineEngine.Engine.Pkg.Etc.Materials;

public class ImageMaterial : IMaterial<string?, Texture2D?>
{
  public Vector2 Size { get; protected set; }
  public Shader? Shader { get; protected set; }
  
  public string? Filename { get; set; }
  public Texture2D? Material { get; set; }

  public ImageMaterial(string filename)
  {
    Filename = filename;
    
    Texture2D texture = Raylib.LoadTexture(filename);
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

  public void Init(GameContext context) { }

  public void Enter(GameContext context)
  {
    if (Material is not null) return;
    Raylib.LoadTexture(Filename);
  }

  public void Leave(GameContext context)
  {
    if (Filename is null && Material is not null) return;
    Raylib.UnloadTexture((Texture2D)Material!);
  }

  public void Update(GameContext context) { }
  
  public void Draw(GameContext context) { }
}