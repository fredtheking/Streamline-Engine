namespace StreamlineEngine.Engine.Pkg.Etc.Interfaces;

public interface IScript
{
  void Init(GameContext context);
  void Enter(GameContext context);
  void Leave(GameContext context);
  void Update(GameContext context);
  void Draw(GameContext context);
}