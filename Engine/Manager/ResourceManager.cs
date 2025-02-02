using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Manager;

public class ResourceManager
{
  public List<dynamic> All { get; } = [];
  
  public IMaterial GetByUuid(string uuid) => All.First(i => i.Uuid == uuid);
  
  #if !RESOURCES
  public void RegisterFromStruct(Context context)
  {
    foreach (var item in typeof(Registration.Materials).GetFields().Select(f => f.GetValue(null)).OfType<IMaterial>().ToArray())
    {
      item.Init(context);
      All.Add(item);
    }
  }
  #endif
}