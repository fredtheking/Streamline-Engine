using ImGuiNET;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Material;

public class SoundMaterial : MaterialTemplate
{
  public float DefaultVolume { get; init; }
  
  public SoundMaterial(int resourceId, float defaultVolume = 1f)
  {
    Id = resourceId;
    DefaultVolume = defaultVolume;
  }

  public override bool Ready() =>
    Material is not null && Raylib.IsSoundValid((Sound)Material);
  
  public override void Load(Context context) =>
    Material = context.Managers.Package.Unpack<Sound>(Id);

  public override void Unload(Context context)
  {
    Raylib.UnloadSound(Material);
    Material = null;
  }
}