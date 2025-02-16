using System.Numerics;
using StreamlineEngine.Engine.Component;
using StreamlineEngine.Engine.Etc.Classes;

namespace StreamlineEngine.Engine.Etc.Builders;

public class TextSettings
{
  public enum TextAlign { Negative, Center, Positive }
  
  public bool Wrap;
  public TextAlign AlignX;
  public TextAlign AlignY;
  public bool Autosize = true;
  public float BetweenLetterSpacing = 3;
  public float LineSpacing = 5;

  private TextSettings() { }

  public class Builder
  {
    private readonly TextSettings _settings = new();
  
    public Builder WrapLines()
    {
      _settings.Wrap = true;
      return this;
    }
    
    public Builder SetAlignAxisX(TextAlign align)
    {
      _settings.AlignX = align;
      return this;
    }

    public Builder SetAlignAxisY(TextAlign align)
    {
      _settings.AlignY = align;
      return this;
    }

    public Builder DisableAutosize()
    {
      _settings.Autosize = false;
      return this;
    }
    
    public Builder SetBetweenLetterSpacing(float space) {
      _settings.BetweenLetterSpacing = space;
      return this;
    }
    
    public Builder SetLineSpacing(float space) {
      _settings.LineSpacing = space;
      return this;
    }

    public TextSettings Build() => _settings;
  }
}