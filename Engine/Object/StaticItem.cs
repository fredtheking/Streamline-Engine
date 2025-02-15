using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.Engine.Object;

public class StaticItem : Item
{
  public StaticItem(string name, params ComponentTemplate[] components) : base(name, components) { }
}