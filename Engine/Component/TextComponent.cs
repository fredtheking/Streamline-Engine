using System.Numerics;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Builders;
using StreamlineEngine.Engine.Etc.Classes;
using StreamlineEngine.Engine.Etc.Templates;
using StreamlineEngine.Engine.Material;
using StreamlineEngine.Engine.Object;

namespace StreamlineEngine.Engine.Component;

public class TextComponent : ComponentTemplate
{
  public PositionComponent LocalPosition { get; set; }
  public PositionComponent Position { get; set; }
  public SizeComponent LocalSize { get; set; }
  public SizeComponent Size { get; set; }
  public FontMaterial Resource { get; set; }
  public RefObj<string> Text { get; set; }
  public bool Changed { get; set; } = true;
  public float FontSize { get; set; }
  public Color Color { get; set; }
  public TextSettings Settings { get; set; }
  public Vector2 MeasuredText { get; set; }
  
  public TextComponent(string text, FontMaterial font, float size, Color color, TextSettings? extra = null)
  {
    Text = new(text);
    Resource = font;
    FontSize = size;
    Color = color;
    Settings = extra ?? new TextSettings.Builder().Build();
    DebugBorderColor = Color.Green;
  }
  
  public TextComponent(RefObj<string> text, FontMaterial font, float size, Color color, TextSettings? extra = null)
  {
    Text = text;
    Resource = font;
    FontSize = size;
    Color = color;
    Settings = extra ?? new TextSettings.Builder().Build();
    DebugBorderColor = Color.Green;
  }

  public Vector2 MeasureText(Context context)
  {
    bool notReady = !Resource.Ready();
    if (notReady) Resource.Load(context);
    MeasuredText = Raylib.MeasureTextEx(Resource.Material, Text.Value, FontSize, Settings.LetterSpacing);
    if (notReady) Resource.Unload(context);
    return MeasuredText;
  }

  public override void Update(Context context)
  {
    if (Changed) WrappedText();
  }

  private void WrappedText()
  {
    string[] words = Text.Value.Split(' ');
    
    List<string> lines = new List<string>();
    string currentLine = "";
    
    while (words.Length > 0)
    {
      while (currentLine.Length < Size.Width)
      {
        currentLine += words[0] + " ";
        words = words.Skip(1).ToArray();
      }
      currentLine.Remove(currentLine.Length - 1);
      lines.Add(currentLine);
      currentLine = "";
    }

    Text.Value = string.Join("\n", lines);
    Changed = false;
  }

  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      Item item = context.Managers.Object.GetByComponent(this);
      
      MeasureText(context);
      Position = item.ComponentTry<PositionComponent>() ?? Error(context, new PositionComponent(), "Item has no position component. Initialising default position.");
      Size = item.ComponentTry<SizeComponent>() ?? Error(context, new SizeComponent(), "Item has no size component. Initialising default size.");
      item.AddMaterials(Resource);
      
      item.LocalLatePosSizeInit(this);
    });
  }

  public override void Draw(Context context)
  {
    Raylib.DrawTextEx(Resource.Material, Text.Value, Position.Vec2 + LocalPosition.Vec2, FontSize, Settings.LetterSpacing, Color);
  }
}