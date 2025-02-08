using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Material;

public class ImageCollectionMaterial : MaterialTemplate
{
  public new int[] Id { get; private set; }
  public new Texture2D[]? Material { get; private set; }
  public Vector2 SharedSize { get; private set; }
  public Vector2[] Size { get; private set; }
  public bool MultiSizeable { get; private set; }
  public Shader? Shader { get; private set; } = null;
  private TextureFilter Filter { get; set; } = TextureFilter.Point;

  public ImageCollectionMaterial(int[] resourceIds, bool loadOnNeedOnly = false, bool multiSizeable = false)
  {
    Id = resourceIds;
    LoadOnNeed = loadOnNeedOnly;
    MultiSizeable = multiSizeable;
  }
  
  public ImageCollectionMaterial(Range resourceIds, bool loadOnNeedOnly = false, bool multiSizeable = false)
  {
    Id = Extra.RangeToArray(resourceIds);
    LoadOnNeed = loadOnNeedOnly;
    MultiSizeable = multiSizeable;
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
    SharedSize = Size.All(s => s == Size[0]) ? Size[0] : !MultiSizeable ? Warning(context, Vector2.Zero, "Collection of images has different sizes. Total size is not initialised.") : Information(context, Vector2.Zero, "Collection is MultiSizeable. Total size is not initialised.");
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
  
  public static ImageCollectionMaterial FromImageMaterial(ImageMaterial material, int repeat = 1) =>
    new ImageCollectionMaterial(Enumerable.Repeat(material.Id, repeat).ToArray(), material.LoadOnNeed);
}