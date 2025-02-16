using Raylib_cs;

namespace StreamlineEngine.Engine.Manager;

public class SettingsManager
{
  public bool VSync
  {
    get => Raylib.IsWindowState(ConfigFlags.VSyncHint);
    set
    {
      if (value) Raylib.SetWindowState(ConfigFlags.VSyncHint);
      else Raylib.ClearWindowState(ConfigFlags.VSyncHint);
    }
  }
}