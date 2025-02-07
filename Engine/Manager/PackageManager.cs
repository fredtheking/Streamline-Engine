using System.Text.Json;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Classes;

namespace StreamlineEngine.Engine.Manager;

public enum ResourceType
{
  ImagePng,
  ImageJpg,
  SoundMp3,
}

public class PackageManager
{
  private static readonly BiDictionary<string, ResourceType> ResourceMap = new()
  {
    { ".png", ResourceType.ImagePng },
    { ".jpg", ResourceType.ImageJpg },
    { ".mp3", ResourceType.SoundMp3 },
  };
  private const string OutputExtension = ".serp";
  private const string IndexExtension = ".seri";
  private const string ResourceName = Config.ResourcesPackageName;
  private const string EnumName = "ResourcesIDs";
  private const string ExportGeneratedPath = "../../../Generated/";
  private readonly long[] _offsetTable;

  public PackageManager(Context context)
  {
    List<long> table = [];
    
    string indexFilename = "Generated/" + ResourceName + IndexExtension;
    if (File.Exists(indexFilename))
    {
      using var indexStream = new FileStream(indexFilename, FileMode.Open);
      byte[] countBytes = new byte[4];
      indexStream.ReadExactly(countBytes, 0, countBytes.Length);
      int count = BitConverter.ToInt32(countBytes, 0);
      for (int i = 0; i < count; i++)
      {
        byte[] offsetBytes = new byte[8];
        indexStream.ReadExactly(offsetBytes, 0, offsetBytes.Length);
        table.Add(BitConverter.ToInt64(offsetBytes));
      }
    } 
    else context.Managers.Debug.Warning("No resource files / index file found. Ignore this warning if you dont plan on using external resources (such as textures, sounds, fonts and so on).");

    _offsetTable = table.ToArray();
  }
  
  public void Pack(Dictionary<string, string> resourceFiles)
  {
    var folderPath = Path.GetDirectoryName(ExportGeneratedPath);
    if (!Directory.Exists(folderPath)) 
      Directory.CreateDirectory(folderPath!);

    using (var fileStream = new FileStream(ExportGeneratedPath + ResourceName + OutputExtension, FileMode.Create))
    using (var indexStream = new FileStream(ExportGeneratedPath + ResourceName + IndexExtension, FileMode.Create))
    using (var enumWriter = new StreamWriter(ExportGeneratedPath + EnumName + ".cs"))
    {
      indexStream.Write(BitConverter.GetBytes(resourceFiles.Count), 0, 4);
      
      enumWriter.WriteLine("namespace StreamlineEngine.Generated;");
      enumWriter.WriteLine($"public enum {Path.GetFileName(EnumName)}");
      enumWriter.WriteLine("{");
      
      long lastOffset = 0;
      int resourceId = 0;
      int maxSpace = resourceFiles.Keys.Max(k => k.Length) + 2;

      foreach (var resourceFile in resourceFiles)
      {
        byte[] resourceData = File.ReadAllBytes(Config.ResourcesPath + resourceFile.Value);
        ResourceType resourceType = ResourceMap[Path.GetExtension(resourceFile.Value).ToLower()];
        
        Span<byte> header = stackalloc byte[8];
        BitConverter.TryWriteBytes(header[..4], resourceData.Length);
        BitConverter.TryWriteBytes(header[4..], (int)resourceType);
        fileStream.Write(header);
        fileStream.Write(resourceData);
        
        byte[] offsetBytes = BitConverter.GetBytes(lastOffset);
        indexStream.Write(offsetBytes, 0, offsetBytes.Length);
        lastOffset += resourceData.Length + 8;
        
        enumWriter.WriteLine($"  {resourceFile.Key} = {resourceId},{new string(' ', maxSpace - resourceFile.Key.Length)}// {resourceType}");
        Console.Write($"Packed '{resourceFile.Key}', Type: {resourceType}, Size: ~{resourceData.Length / 1024}KB, ID: {resourceId}");
        Console.CursorLeft = Console.WindowWidth - $"{resourceId + 1}/{resourceFiles.Count}".Length;
        Console.WriteLine($"{resourceId + 1}/{resourceFiles.Count}");
        resourceId++;
      }
      
      enumWriter.WriteLine("}");
    }
  }
  
  public T Unpack<T>(int resourceID)
  {
    string resourcesFilename = "Generated/" + ResourceName + OutputExtension;
    if (!File.Exists(resourcesFilename))
      throw new FileNotFoundException();
    
    if (resourceID >= _offsetTable.Length)
      throw new IndexOutOfRangeException("Resource ID is out of range");

    using var fileStream = new FileStream(resourcesFilename, FileMode.Open);
    fileStream.Seek(_offsetTable[resourceID], SeekOrigin.Begin);
    
    Span<byte> headerBuffer = stackalloc byte[8];
    
    fileStream.ReadExactly(headerBuffer);
    int sizeLength = BitConverter.ToInt32(headerBuffer[..4]);
    ResourceType resourceType = (ResourceType)BitConverter.ToInt32(headerBuffer[4..]);
      
    byte[] resourceData = new byte[sizeLength];
    fileStream.ReadExactly(resourceData, 0, sizeLength);
    return LoadResourceByType<T>(resourceData, resourceType);
  }
  
  public T[] UnpackMany<T>(int[] resourceIDs)
  {
    string resourcesFilename = "Generated/" + ResourceName + OutputExtension;
    if (!File.Exists(resourcesFilename))
      throw new FileNotFoundException();

    if (resourceIDs.Any(id => id >= _offsetTable.Length))
      throw new IndexOutOfRangeException("At least one of Resource IDs is out of range");

    // 1. Сортируем ресурсные ID
    Array.Sort(resourceIDs);

    List<T> resources = new();
    using var fileStream = new FileStream(resourcesFilename, FileMode.Open, FileAccess.Read);

    long lastOffset = -1;
    Span<byte> headerBytes = stackalloc byte[8];
    foreach (int resourceID in resourceIDs)
    {
      long offset = _offsetTable[resourceID];
      
      if (offset != lastOffset)
        fileStream.Position = offset;

      lastOffset = offset;
      
      fileStream.ReadExactly(headerBytes);

      int sizeLength = BitConverter.ToInt32(headerBytes[..4]);
      ResourceType resourceType = (ResourceType)BitConverter.ToInt32(headerBytes[4..]);

      byte[] resourceData = GC.AllocateUninitializedArray<byte>(sizeLength);
      fileStream.ReadExactly(resourceData);

      resources.Add(LoadResourceByType<T>(resourceData, resourceType));

      lastOffset += sizeLength + 8;
    }

    return resources.ToArray();
  }


  public static Dictionary<string, string> GetJsonToPackAsDict(string filename)
  {
    string json = File.ReadAllText(filename);
    Dictionary<string, string>? dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    if (dict == null)
      throw new NullReferenceException("Resources JSON is empty!");
    return dict;
  }
  
  private T LoadResourceByType<T>(byte[] resourceData, ResourceType resourceType)
  {
    string ext = ResourceMap[resourceType];
    if (typeof(T) == typeof(Image)) 
      return (T)(object)Raylib.LoadImageFromMemory(ext, resourceData);
    if (typeof(T) == typeof(Texture2D))
    {
      Image image = Raylib.LoadImageFromMemory(ext, resourceData);
      Texture2D texture = Raylib.LoadTextureFromImage(image);
      Raylib.UnloadImage(image);
      return (T)(object)texture;
    }
    if (typeof(T) == typeof(Wave))
      return (T)(object)Raylib.LoadWaveFromMemory(ext, resourceData);
    if (typeof(T) == typeof(Sound))
    {
      Wave wave = Raylib.LoadWaveFromMemory(ext, resourceData);
      Sound sound = Raylib.LoadSoundFromWave(wave);
      Raylib.UnloadWave(wave);
      return (T)(object)sound;
    }
    throw new InvalidOperationException("Invalid resource type");
  }
}