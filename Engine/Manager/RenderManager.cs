using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Manager;

public class RenderManager
{
  public Dictionary<Item, (Action<Context>, RefObj<int>)> All { get; } = [];
  public List<Item> CurrentFrame { get; } = [];

  public void Render(Context context)
  {
    List<(Action<Context>, RefObj<int>)> NewOrder = [];
    foreach (Item item in CurrentFrame) 
      NewOrder.Add(All[item]);
    foreach ((Action<Context>, RefObj<int>) tuple in NewOrder.OrderBy(p => p.Item2.Value))
      tuple.Item1(context);
    CurrentFrame.Clear();
  }
}