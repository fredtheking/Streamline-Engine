using StreamlineEngine.Engine.EntityMaterial;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine.Engine.Pkg.Manager;

public class Scene : IScript
{
  public required string Name { get; set; }
  public required int Id { get; set; }
  public Dictionary<string, StaticEntity> Entities = [];
  public void Init(GameContext context) =>
    throw new Exception("Should not be called");

  public void Enter(GameContext context)
  {
    foreach (StaticEntity entity in Entities.Values)
      entity.Enter(context);
  }
  
  public void Leave(GameContext context)
  {
    foreach (StaticEntity entity in Entities.Values)
      entity.Leave(context);
  }

  public void Update(GameContext context)
  {
    foreach (StaticEntity entity in Entities.Values)
      entity.Update(context);
  }

  public void Draw(GameContext context)
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

  private void PrePostChange(GameContext context, Action action)
  {
    Current?.Leave(context);
    action();
    Changed = true;
  }
  
  public void Change(GameContext context, Config.Scenes scene) => PrePostChange(context, () => Current = All[(int)scene]);
  public void Change(GameContext context, int sceneId) => PrePostChange(context, () => Current = All[sceneId]);
  public void Change(GameContext context, string sceneName) => PrePostChange(context, () => Current = All[Array.FindIndex(All, s => s.Name == sceneName)]);
}