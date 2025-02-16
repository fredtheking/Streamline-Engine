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
  public string[] LinedText { get; private set; }
  public float FontSize { get; set; }
  public Color Color { get; set; }
  public TextSettings Settings { get; set; }
  public Vector2 OneSymbolSize { get; set; }
  
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

  public Vector2 MeasureText(Context context, string msg)
  {
    bool notReady = !Resource.Ready();
    if (notReady) Resource.Load(context);
    Vector2 measure = Raylib.MeasureTextEx(Resource.Material, msg, FontSize, Settings.LetterSpacing);
    if (notReady) Resource.Unload(context);
    return measure;
  }

  public override void Init(Context context)
  {
    InitOnce(() =>
    {
      Item item = context.Managers.Object.GetByComponent(this);
      
      OneSymbolSize = MeasureText(context, "A");
      Position = item.ComponentTry<PositionComponent>() ?? Error(context, new PositionComponent(), "Item has no position component. Initialising default position.");
      Size = item.ComponentTry<SizeComponent>() ?? Error(context, new SizeComponent(), "Item has no size component. Initialising default size.");
      item.AddMaterials(Resource);
      
      item.LocalLatePosSizeInit(this);
    });
  }

  public override void Update(Context context)
  {
    string[] words = Text.Value.Split(' ');
    
    List<string> lines = [];
    string currentLine = "";
    
    foreach (string word in words)
    {
      string temp = currentLine + word + " ";
      if (MeasureText(context, temp[..^1]).X >= Size.Width + LocalSize.Width)
      {
        if (currentLine != "") currentLine = currentLine[..^1];
        lines.Add(currentLine);
        currentLine = word + " ";
      }
      else currentLine = temp;
    }
    lines.Add(currentLine);
    
    if (lines[0] == "") lines.RemoveAt(0);
    LinedText = lines.ToArray();
  }

  public override void Draw(Context context)
  {
    float offsetY = 0;
    foreach (string line in LinedText)
    {
      Raylib.DrawTextEx(Resource.Material, line, Position.Vec2 + LocalPosition.Vec2 + new Vector2(0, offsetY), FontSize, Settings.LetterSpacing, Color);
      offsetY += OneSymbolSize.Y + Settings.LineSpacing;
    }
  }
}