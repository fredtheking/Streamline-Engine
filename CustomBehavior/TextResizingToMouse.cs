using Raylib_cs;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Templates;

namespace StreamlineEngine.CustomBehavior;

public class TextResizingToMouse : CustomItemBehaviorTemplate
{
  PositionComponent _pos;
  SizeComponent _size;
  
  public override void Init(Context context)
  {
    _pos = Parent.Component<PositionComponent>();
    _size = Parent.Component<SizeComponent>();
  }

  public override void Update(Context context)
  {
    _size.Width = Math.Abs(Raylib.GetMouseX() - _pos.X);
    _size.Height = Math.Abs(Raylib.GetMouseY() - _pos.Y);
  }
}