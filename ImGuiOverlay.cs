using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine;

public class ImGuiOverlay : IScript
{
  private static Vector2 LimitsY = new(-375, 5);
  private float AnchorPointY = LimitsY.X;
  private float FollowPointY = LimitsY.X;
  private bool Show;
  
  public void Switch() => Show = !Show;
  public void Init(Context context) { }
  public void CheckInitCorrect(Context context) { }
  public void Enter(Context context) { }
  public void Leave(Context context) { }
  public void EarlyUpdate(Context context) { }
  public void Update(Context context) { }
  public void LateUpdate(Context context) { }
  public void Draw(Context context)
  {
    Raylib.DrawText($"Work in Progress [ {Config.Version} ] : This is not a final product. Build time: {Math.Round(Raylib.GetTime(), 1).ToString().PadRight(5, '-')}. Fps: {Raylib.GetFPS().ToString().PadLeft(4, '-')}. '~' for debugger.", 5, (int)(Config.WindowSize.Y - 20), 12, new Color(255, 255, 255, 69));
    
    AnchorPointY = Show && context.Managers.Debug.TurnedOn ? 5 : -395;
    FollowPointY = Raymath.Lerp(FollowPointY, AnchorPointY, 20 * Raylib.GetFrameTime());
    if (FollowPointY <= LimitsY.X - 3) return;
    
    ImGui.Begin("Debugger", ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse);
    
    if (context.Managers.Debug.Changed) ImGui.SetWindowFocus();
    ImGui.SetWindowSize(new Vector2(Config.WindowSize.X - 10, 400), ImGuiCond.Always);
    ImGui.SetWindowPos(new Vector2(5, FollowPointY), ImGuiCond.Always);

    ImGui.Text("Hello World!");
    ImGui.End();
  }
}
