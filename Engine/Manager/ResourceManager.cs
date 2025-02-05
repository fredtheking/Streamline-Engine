using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Manager;

public class ResourceManager
{
  public List<MaterialTemplate> All { get; } = [];
  
  public MaterialTemplate GetByUuid(string uuid) => All.First(i => i.Uuid == uuid);
  
  #if !RESOURCES
  public void RegisterFromStruct(Context context)
  {
    foreach (var mat in typeof(Registration.Materials).GetFields().Select(f => f.GetValue(null)).OfType<MaterialTemplate>().ToArray())
    {
      mat.Init(context);
      All.Add(mat);
    }
  }
  #endif
}