using Raylib_cs;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.CustomBehavior;

public class BonnieScript : CustomItemBehaviorTemplate
{
  private PositionComponent _position;
  private PositionComponent _position2;
  private LayerComponent _layer;
  
  public override void Init(Context context)
  {
    #if !RESOURCES
    _position = Parent.Component<PositionComponent>();
    _position2 = Registration.Items.Item2Helper.Component<PositionComponent>();
    _layer = Parent.Component<LayerComponent>();
    #endif
  }

  public override void Update(Context context)
  {
    _position.Set(Raylib.GetMousePosition());

    _layer.Layer.Value = _position.Y < _position2.Y - 100 ? 0 : 1;
  }
}