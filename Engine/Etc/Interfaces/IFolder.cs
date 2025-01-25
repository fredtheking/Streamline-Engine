namespace StreamlineEngine.Engine.Etc.Interfaces;

public interface IFolder<TChildren>
{
  public string Name { get; protected set; }
  public bool Active { get; set; }
  public List<dynamic>? Parent { get; protected set; }
  public List<TChildren>? Children { get; protected set; }
}