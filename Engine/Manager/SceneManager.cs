using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Pkg.Manager;

public class Scene : IScript
{
  public required string Name { get; set; }
  public required int Id { get; set; }
  public Dictionary<string, StaticEntity> Entities = [];
  public void Init(MainContext context) =>
    throw new Exception("Should not be called");

  public void Enter(MainContext context)
  {
    foreach (StaticEntity entity in Entities.Values)
      entity.Enter(context);
  }
  
  public void Leave(MainContext context)
  {
    foreach (StaticEntity entity in Entities.Values)
      entity.Leave(context);
    context.Global.Leave(context);
  }

  public void Update(MainContext context)
  {
    foreach (StaticEntity entity in Entities.Values)
      entity.Update(context);
  }

  public void Draw(MainContext context)
  {
    foreach (StaticEntity entity in Entities.Values)
      entity.Draw(context);
  }
}

public class SceneManager
{
  public bool Changed { get; set; }
  public Scene? Current { get; private set; }
  public Scene[] All { get; }

  public SceneManager()
  {
    All = Enum.GetValues<Config.Scenes>()
      .Select(s => new Scene { Name = s.ToString(), Id = (int)s})
      .ToArray();
    Current = All[(int)Config.StartScene];
    Changed = true;
  }

  private void PrePostChange(MainContext context, Action action)
  {
    context.Managers.Debug.PrintSeparator(ConsoleColor.Blue, $"Changing from '{Current!.Name}' scene and loading resources...");
    Current?.Leave(context);
    action();
    Changed = true;
    Current!.Enter(context);
    context.Managers.Debug.PrintSeparator(ConsoleColor.Green, $"Successfully changed scene to '{Current!.Name}'!");
  }
  
  public void Change(MainContext context, Config.Scenes scene) => PrePostChange(context, () => Current = All[(int)scene]);
  public void Change(MainContext context, int sceneId) => PrePostChange(context, () => Current = All[sceneId]);
  public void Change(MainContext context, string sceneName) => PrePostChange(context, () => Current = All[Array.FindIndex(All, s => s.Name == sceneName)]);
  
  public void Previous(MainContext context) => Change(context, (Array.FindIndex(All, s => s == Current) - 1 + All.Length) % All.Length);
  public void Next(MainContext context) => Change(context, (Array.FindIndex(All, s => s == Current) + 1) % All.Length);
}