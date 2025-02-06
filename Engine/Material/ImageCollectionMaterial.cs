using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Material;

public class ImageCollectionMaterial : MaterialTemplate
{
  public int[] Id { get; private set; }
  public Texture2D[]? Material { get; private set; }
  public Vector2[] Size { get; private set; }
  public Shader? Shader { get; private set; } = null;
  private TextureFilter Filter { get; set; } = TextureFilter.Point;

  public ImageCollectionMaterial(int[] resourceIds, bool loadOnNeedOnly = false)
  {
    Id = resourceIds;
    LoadOnNeed = loadOnNeedOnly;
  }
  
  public ImageCollectionMaterial(Range resourceIds, bool loadOnNeedOnly = false)
  {
    Id = Extra.RangeToArray(resourceIds);
    LoadOnNeed = loadOnNeedOnly;
  }

  public void AddFilter(TextureFilter filter) => Filter = filter;
  public void AddShader(Shader shader) => Shader = shader;

  public override void Init(Context context)
  {
    Texture2D[] textures = context.Managers.Package.UnpackMany<Texture2D>(Id);
    List<Vector2> sizes = [];
    foreach (Texture2D texture in textures)
    {
      sizes.Add(new Vector2(texture.Width, texture.Height));
      Raylib.UnloadTexture(texture);
    }
    Size = sizes.ToArray();
  }

  public override bool Ready() => Material is not null && Material.All(m => Raylib.IsTextureValid(m));

  public override void Load(Context context)
  {
    Material = context.Managers.Package.UnpackMany<Texture2D>(Id);
    Material.ToList().ForEach(m => Raylib.SetTextureFilter(m, Filter));
  }

  public override void Unload(Context context)
  {
    Material!.ToList().ForEach(Raylib.UnloadTexture);
    Material = null;
  }
}