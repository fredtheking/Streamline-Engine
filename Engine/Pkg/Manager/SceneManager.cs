using StreamlineEngine.Engine.Pkg.ECS.EntityDir;
using StreamlineEngine.Engine.Pkg.Interfaces;

namespace StreamlineEngine.Engine.Pkg.Manager;

public class Scene : IScript
{
  public required string Name { get; set; }
  public required int Id { get; set; }
  public Dictionary<string, Entity> Entities = [];
  public void Init(GameContext context) =>
    throw new Exception("Should not be called");

  public void Enter(GameContext context)
  {
    foreach (Entity entity in Entities.Values)
      entity.Enter(context);
  }

  public void Update(GameContext context)
  {
    foreach (Entity entity in Entities.Values)
      entity.Update(context);
  }

  public void Draw(GameContext context)
  {
    foreach (Entity entity in Entities.Values)
      entity.Draw(context);
  }
}

public class SceneManager
{
  public bool Changed { get; private set; }
  public Scene Current { get; private set; }
  public Scene[] All { get; }

  public SceneManager()
  {
    All = Enum.GetValues<Config.Scenes>()
      .Select(s => new Scene { Name = s.ToString(), Id = (int)s})
      .ToArray();
    Current = All[(int)Config.StartScene];
    Changed = true;
  }

  private void PrePostChange(Action action)
  {
    action();
    Changed = true;
  }
  
  public void Change(Config.Scenes scene) => PrePostChange(() => Current = All[(int)scene]);
  public void Change(int sceneId) => PrePostChange(() => Current = All[sceneId]);
  public void Change(string sceneName) => PrePostChange(() => Current = All[Array.FindIndex(All, s => s.Name == sceneName)]);
}