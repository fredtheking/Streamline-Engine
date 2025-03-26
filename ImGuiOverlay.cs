#if !RESOURCES
using System.Diagnostics;
using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Engine.Etc.Interfaces;

namespace StreamlineEngine;

public class ImGuiOverlay : IScript
{
  internal enum Modes
  {
    MainInfo,
    TreeProperties,
  }

  public bool Show { get; private set; } = false;
  public List<Action<Context>> CurrentTreeInfo { get; } = [];
  
  private static Vector2 LimitsY = new(-389, 5);
  private float AnchorPointY = LimitsY.X;
  private float FollowPointY = LimitsY.X;
  private Modes State;

  private List<RunningTextObject> RunningTextObjects = [new(Config.WindowSize.X)];
  
  private long MemoryUsed = Process.GetCurrentProcess().WorkingSet64;
  private SeTimer MemoryUpdateTimer = new SeTimer(1.69f, startOnCreate: true);

  private bool MainInfoFpsLimit_VSync;
  private bool MainInfoFpsLimit_Custom;
  private int MainInfoFpsLimit_CustomValue = 120;
  private bool MainInfoFpsLimit_None = true;
  
  public void Switch() => Show = !Show;
  public void Init(Context context) { }
  public void CheckInitCorrect(Context context) { }
  public void Enter(Context context) { }
  public void Leave(Context context) { }
  public void EarlyUpdate(Context context) { }
  public void Update(Context context) { if (Show) OnlyDropdownUpdate(context); }
  public void LateUpdate(Context context) { }
  private void OnlyDropdownUpdate(Context context)
  {
    if (context.Managers.Keybind.IsKeyPressed(KeyboardKey.Backspace) && CurrentTreeInfo.Count > 1) CurrentTreeInfo.Remove(CurrentTreeInfo[^1]);
  }
  public void Draw(Context context)
  {
    AnchorPointY = Show && context.Managers.Debug.TurnedOn ? LimitsY.Y : LimitsY.X;
    FollowPointY = Raymath.Lerp(FollowPointY, AnchorPointY, 20f * Raylib.GetFrameTime());
    FollowPointY = Math.Clamp(FollowPointY, LimitsY.X, LimitsY.Y);
    
    if (FollowPointY <= LimitsY.X + 3) return;
    
    var target = Math.Pow(1f - (FollowPointY + LimitsY.X) / (LimitsY.Y + LimitsY.X) + 1f, .5f);
    ImGui.PushStyleVar(ImGuiStyleVar.Alpha, (float)target);
    ImGui.Begin("Debugger", ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse);
    
    if (context.Managers.Debug.Changed) ImGui.SetWindowFocus();
    ImGui.SetWindowSize(new Vector2(Config.WindowSize.X - 10, 400), ImGuiCond.Always);
    ImGui.SetWindowPos(new Vector2(5, FollowPointY), ImGuiCond.Always);
    
    if (ImGui.BeginTable("Main", 1, ImGuiTableFlags.Borders | ImGuiTableFlags.NoPadOuterX | ImGuiTableFlags.NoPadInnerX))
    {
      ImGui.TableNextRow(ImGuiTableRowFlags.Headers);
      ImGui.TableSetColumnIndex(0);
      ImGui.BeginGroup();
      var names = Enum.GetNames(typeof(Modes));
      for (int i = 0; i < names.Length; i++)
      {
        if (ImGui.Button(names[i])) 
          State = (Modes)i;
        ImGui.SameLine();
      }
      ImGui.EndGroup();
      
      ImGui.TableNextRow(ImGuiTableRowFlags.None, ImGui.GetWindowHeight() - 39);
      ImGui.TableSetColumnIndex(0);
      
      RealDraw(context);
      
      ImGui.EndTable();
    }
    
    ImGui.End();
    ImGui.PopStyleVar();
  }

  private bool GenerateCheckbox(string label, bool value)
  {
    var keep = value;
    ImGui.Checkbox(label, ref keep);
    return keep;
  }

  private void CenterNextGroupY() =>
    ImGui.SetCursorPosY((ImGui.GetContentRegionAvail().Y - ImGui.GetItemRectSize().Y) / 2);

  private void RealDraw(Context context)
  {
    if (ImGui.BeginTable("Subtable", 2, ImGuiTableFlags.Borders))
    {
      ImGui.TableNextRow(ImGuiTableRowFlags.None, 333);
      switch (State)
      {
        case Modes.MainInfo:
          ImGui.TableSetColumnIndex(0);
          
          MemoryUpdateTimer.Update();
          if (MemoryUpdateTimer.Target()) MemoryUsed = Process.GetCurrentProcess().WorkingSet64;
          
          CenterNextGroupY();
          ImGui.BeginGroup();
          ImGui.Text($"Time since start: {Raylib.GetTime()}");
          ImGui.Text($"FPS: {Raylib.GetFPS()}");
          ImGui.Text($"MS: {Raylib.GetFrameTime()}");
          ImGui.Text($"Memory used: {MemoryUsed / 1024 / 1024} MB");
          ImGui.EndGroup();

          ImGui.TableSetColumnIndex(1);
          CenterNextGroupY();
          ImGui.BeginGroup();
          context.Managers.Debug.TurnedOn = GenerateCheckbox("Debug mode", context.Managers.Debug.TurnedOn);
          context.Managers.Debug.ShowBorders = GenerateCheckbox("Show Border", context.Managers.Debug.ShowBorders);
          Show = GenerateCheckbox("Show this menu", Show);
          
          ImGui.Separator();
          
          if (ImGui.RadioButton("Vsync", MainInfoFpsLimit_VSync))
          {
            MainInfoFpsLimit_VSync = true;
            MainInfoFpsLimit_Custom = false;
            MainInfoFpsLimit_None = false;
            context.Managers.Settings.VSync = MainInfoFpsLimit_VSync;
            Raylib.SetTargetFPS(-1);
          }
          if (ImGui.RadioButton("Custom", MainInfoFpsLimit_Custom))
          {
            MainInfoFpsLimit_VSync = false;
            MainInfoFpsLimit_Custom = true;
            MainInfoFpsLimit_None = false;
            context.Managers.Settings.VSync = MainInfoFpsLimit_VSync;
            Raylib.SetTargetFPS(MainInfoFpsLimit_CustomValue);
          }
          ImGui.SameLine();
          ImGui.SetNextItemWidth(96);
          ImGui.InputInt("Limit", ref MainInfoFpsLimit_CustomValue);
          if (ImGui.RadioButton("None", MainInfoFpsLimit_None))
          {
            MainInfoFpsLimit_VSync = false;
            MainInfoFpsLimit_Custom = false;
            MainInfoFpsLimit_None = true;
            context.Managers.Settings.VSync = MainInfoFpsLimit_VSync;
            Raylib.SetTargetFPS(-1);
          }
          
          ImGui.EndGroup();
          break;
        case Modes.TreeProperties:
          ImGui.TableSetColumnIndex(0);
          context.Root.DebuggerTree(context);
          ImGui.TableSetColumnIndex(1);
          if (CurrentTreeInfo.Count > 0) CurrentTreeInfo[^1](context);
          break;
      }
      ImGui.EndTable();
      RunningText();
    }
  }

  private void RunningText()
  {
    RunningTextObject[] initial = RunningTextObjects.ToArray();
    foreach (RunningTextObject obj in initial)
    {
      obj.Update(RunningTextObjects);
      obj.Draw();
    }
  }
}

internal class RunningTextObject
{
  public float Offset;
  public float Size;
  public string Text;
  public bool CreatedChild;

  public RunningTextObject(float offset)
  {
    Text = Defaults.RunningPhrases[new Random().Next(Defaults.RunningPhrases.Length)] + "\t\t\t\t|\t\t\t\t";
    Offset = offset;
  }

  public void Update(List<RunningTextObject> objs)
  {
    Offset -= 123f * Raylib.GetFrameTime();
    Size = ImGui.CalcTextSize(Text).X;
    
    if (Offset + Size < 0)
      objs.Remove(this);
    
    if (!CreatedChild && Offset < ImGui.GetWindowWidth() - Size)
    {
      CreatedChild = true;
      RunningTextObject lastObj = objs.Last(); 
      objs.Add(new RunningTextObject(lastObj.Offset + lastObj.Size));
    } 
  }
  
  public void Draw()
  {
    ImGui.SetCursorPosX(Offset);
    ImGui.Text(Text);
    ImGui.SameLine();
  }
}
#endif
