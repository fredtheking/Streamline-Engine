using Raylib_cs;
namespace StreamlineEngine.Engine.Etc.Classes;

public enum OnTargetTimeAchieved { Stop, Loop, Continue, Die }

public class SeTimer
{
  public double StartTime { get; set; }
  public double CurrentTime { get; set; }
  public double TargetTime { get; set; }

  public TimeState State { get; private set; }
  public bool StartOnCreate { get; set; }
  public OnTargetTimeAchieved OnTarget { get; set; }

  public SeTimer(double targetTimeInSeconds, OnTargetTimeAchieved action = OnTargetTimeAchieved.Loop, bool startOnCreate = false)
  {
    TargetTime = targetTimeInSeconds;
    OnTarget = action;
    StartOnCreate = startOnCreate;
    if (StartOnCreate) Activate();
  }

  public void Activate()
  {
    if (State is TimeState.Dead) return;
    Reset();
    State = TimeState.Running;
    StartTime = Raylib.GetTime();
  }

  public void FactoryReset() { Stop(); Reset(); }
  public void Stop() => State = TimeState.Idle;
  public void Kill() => State = TimeState.Dead;
  public void Reset() => CurrentTime = 0;
  
  public void Update()
  {
    if (State is TimeState.Running) CurrentTime = Raylib.GetTime() - StartTime;
  }
  
  public bool Target() {
    if (CurrentTime < TargetTime) return false;
    else
    {
      switch (OnTarget)
      {
        case OnTargetTimeAchieved.Stop:
          Stop();
          break;
        case OnTargetTimeAchieved.Loop:
          Activate();
          break;
        case OnTargetTimeAchieved.Die:
          Kill();
          break;
      }
      return true;
    }
  }
}