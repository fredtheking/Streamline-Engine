using System.Numerics;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc.Classes;

namespace StreamlineEngine.Engine.Etc.Builders;

public class TextSettings
{
  public bool Wrap;
  public bool CenterOnX;
  public bool CenterOnY;
  public bool Autosize = true;
  public float LetterSpacing = 3;
  public float LineSpacing = 5;

  private TextSettings() { }

  public class Builder
  {
    private readonly TextSettings _settings = new TextSettings();
  
    public Builder WrapLines()
    {
      _settings.Wrap = true;
      return this;
    }
    
    public Builder CenterOnX()
    {
      _settings.CenterOnX = true;
      return this;
    }

    public Builder CenterOnY()
    {
      _settings.CenterOnY = true;
      return this;
    }

    public Builder DisableAutosize()
    {
      _settings.Autosize = false;
      return this;
    }
    
    public Builder SetLetterSpacing(float space) {
      _settings.LetterSpacing = space;
      return this;
    }
    
    public Builder SetLineSpacing(float space) {
      _settings.LineSpacing = space;
      return this;
    }

    public TextSettings Build() => _settings;
  }
}