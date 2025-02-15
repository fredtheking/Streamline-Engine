using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Manager;

public class RenderManager
{
  public Dictionary<Item, (Action<Context>, int)> All { get; } = [];
  public List<Item> CurrentFrame { get; } = [];

  public void Render(Context context)
  {
    List<(Action<Context>, int)> NewOrder = [];
    foreach (Item item in CurrentFrame) 
      NewOrder.Add(All[item]);
    foreach ((Action<Context>, int) tuple in NewOrder.OrderBy(p => p.Item2))
      tuple.Item1(context);
    CurrentFrame.Clear();
  }
}