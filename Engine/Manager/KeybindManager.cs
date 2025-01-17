using Raylib_cs;

namespace StreamlineEngine.Engine.Manager;

public class KeybindManager
{
  public bool IsKeyPressed(KeyboardKey key) => Raylib.IsKeyPressed(key);
  public bool IsKeyHold(KeyboardKey key) => Raylib.IsKeyDown(key);
  public bool IsKeyReleased(KeyboardKey key) => Raylib.IsKeyReleased(key);
  
  public KeyboardKey GetKeyPressed() => (KeyboardKey)Raylib.GetKeyPressed();
}