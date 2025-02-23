using Raylib_cs;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.CustomBehavior;

public class AddPositionOnClick : CustomItemBehaviorTemplate
{
  private PositionComponent _position;
  private MouseHitboxComponent _hitbox;
  private SoundComponent _sound;
  
  public override void Init(Context context)
  {
    _position = Parent.Component<PositionComponent>();
    _hitbox = Parent.Component<MouseHitboxComponent>();
    _sound = Parent.Component<SoundComponent>();
  }

  public override void Update(Context context)
  {
    if (_hitbox.Click[(int)MouseButton.Left] || _hitbox.Click[(int)MouseButton.Right])
      _sound.Play();
    
    if (_hitbox.Click[(int)MouseButton.Left])
      _position.Add(10, 0);
    else if (_hitbox.Click[(int)MouseButton.Right])
      _position.Add(-10, 0);
  }
}