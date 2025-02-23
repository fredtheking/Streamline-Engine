using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Material;

namespace StreamlineEngine.Engine.Component;

public class SoundComponent : ComponentTemplate
{
  public SoundMaterial Resource { get; init; }
  public bool Loop { get; set; }
  public bool OverrideSound { get; set; }
  public bool StopOnLeave { get; set; }
  public SoundPlayingState State { get; private set; }
  private bool _paused;

  public SoundComponent(SoundMaterial sound, bool loop = false, bool overrideSound = true, bool stopOnLeave = true)
  {
    Resource = sound;
    Loop = loop;
    OverrideSound = overrideSound;
    StopOnLeave = stopOnLeave;
  }

  public void Play() => State = SoundPlayingState.Started;
  public void Stop() => State = SoundPlayingState.Stopped;
  public void Pause() => State = SoundPlayingState.Paused;
  public void Resume() => State = SoundPlayingState.Playing;

  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      context.Managers.Object.GetByComponent(this).AddMaterials(Resource);
    });
  }

  public override void Update(Context context)
  {
    switch (State)
    {
      case SoundPlayingState.Stopped:
        if (Raylib.IsSoundPlaying(Resource.Material)) Raylib.StopSound(Resource.Material);
        _paused = false;
        break;
      case SoundPlayingState.Started:
        if (OverrideSound) Raylib.StopSound(Resource.Material);
        Raylib.PlaySound(Resource.Material);
        State = SoundPlayingState.Playing;
        _paused = false;
        break;
      case SoundPlayingState.Playing:
        if (!Raylib.IsSoundPlaying(Resource.Material) && _paused)
          Raylib.ResumeSound(Resource.Material);
        _paused = false;
        break;
      case SoundPlayingState.Paused:
        if (Raylib.IsSoundPlaying(Resource.Material)) Raylib.PauseSound(Resource.Material);
        _paused = true;
        break;
      case SoundPlayingState.Ended:
        if (Loop) Play();
        else Stop();
        break;
    }
    
    if (!Raylib.IsSoundPlaying(Resource.Material) && State == SoundPlayingState.Playing) 
      State = SoundPlayingState.Ended;
  }

  public override void Leave(Context context)
  {
    if (StopOnLeave) Raylib.StopSound(Resource.Material);
  }
}