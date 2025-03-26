using Raylib_cs;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.CustomBehavior;

public class PressChange : CustomItemBehaviorTemplate
{
  private PositionComponent _position;
  private MouseHitboxComponent _hitbox;
  private BorderComponent _border;
  private FillComponent _fill;
  private SoundComponent _sound;
  private TextComponent _text;
  
  Color[] colors;
  Color[] borderColors;
  Color[] fillColors;
  bool selected;

  public override void Init(Context context)
  {
    colors = [Defaults.DebugHitboxColor, new(0, 0, 255, 75)];
    borderColors = [Color.Red, Color.Blue];
    fillColors = [new Color(255, 200, 200), new Color(200, 200, 255)];

    _position = Parent.Component<PositionComponent>();
    _hitbox = Parent.Component<MouseHitboxComponent>();
    _border = Parent.Component<BorderComponent>();
    _fill = Parent.Component<FillComponent>();
    _sound = Parent.Component<SoundComponent>();
    _text = Parent.Component<TextComponent>();
  }

  public override void Update(Context context)
  {
    _position.Set((float)Math.Sin(Raylib.GetTime())*200 + 500, (float)Math.Cos(Raylib.GetTime())*100 + 200);
    
    if (_hitbox.Click[(int)MouseButton.Left])
      _sound.Play();
    if (_hitbox.Click[(int)MouseButton.Middle])
      _sound.Stop();

    if (_hitbox.Click[(int)MouseButton.Right])
    {
      if (_sound.State != SoundPlayingState.Paused)
        _sound.Pause();
      else if (_sound.State == SoundPlayingState.Paused)
        _sound.Resume();
    }

    if (_hitbox.Hover)
    {
      _sound.Volume += Raylib.GetMouseWheelMoveV().Y/10f;
    }
    _sound.Volume = Math.Round(_sound.Volume, 1);
    
    selected = _hitbox.Drag[(int)MouseButton.Left];
    _hitbox.Color = colors[selected ? 1 : 0];
    _border.Color = borderColors[selected ? 1 : 0];
    _fill.Color = fillColors[selected ? 1 : 0];
    _text.Text.Value = $"{_sound.State}\nVol: {_sound.Volume*100}%";
  }
}