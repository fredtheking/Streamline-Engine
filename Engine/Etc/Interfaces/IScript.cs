namespace StreamlineEngine.Engine.Etc.Interfaces;

public interface IScript
{
  void Init(Context context);
  void CheckInitCorrect(Context context);
  void Enter(Context context);
  void Leave(Context context);
  void EarlyUpdate(Context context);
  void Update(Context context);
  void LateUpdate(Context context);
  void Draw(Context context);
}