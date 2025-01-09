namespace StreamlineEngine.Engine.Etc.Interfaces;

public interface IScript
{
  void Init(MainContext context);
  void Enter(MainContext context);
  void Leave(MainContext context);
  void Update(MainContext context);
  void Draw(MainContext context);
}