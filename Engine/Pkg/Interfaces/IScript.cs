namespace StreamlineEngine.Engine.Pkg.Interfaces;

public interface IScript
{
  void Init(GameContext context) { }
  void Enter(GameContext context) { }
  void Update(GameContext context) { }
  void Draw(GameContext context) { }
}