using System.Text.Json;
using System.Text.RegularExpressions;
using Raylib_cs;
using StreamlineEngine.Engine.Etc;
using StreamlineEngine.Engine.Etc.Classes;

namespace StreamlineEngine.Engine.Manager;

public enum ResourceType
{
  ImagePng,
  ImageJpg,
  SoundMp3,
  SoundWav,
  FontTtf,
  ShaderVertex,
  ShaderFragment
}

public class PackageManager
{
  private static readonly BiDictionary<string, ResourceType> ResourceMap = new()
  {
    { ".png", ResourceType.ImagePng },
    { ".jpg", ResourceType.ImageJpg },
    { ".mp3", ResourceType.SoundMp3 },
    { ".wav", ResourceType.SoundWav },
    { ".ttf", ResourceType.FontTtf },
    { ".vs", ResourceType.ShaderVertex },
    { ".fs", ResourceType.ShaderFragment }
  };
  private const string OutputExtension = ".serp";
  private const string IndexExtension = ".seri";
  private const string ResourceName = Config.ResourcesPackageName;
  private const string StructName = "ResourcesIDs";
  private const string ExportGeneratedPath = "../../../Generated/";
  private readonly long[] _offsetTable;
  private FileStream _fileStream;

#if RESOURCES
  private long _lastOffset = 0;
  private int _resourceId = 0;
  #endif

  public PackageManager(Context context)
  {
    List<long> table = [];
    #if !RESOURCES
    
    string fileFilename = "Generated/" + ResourceName + OutputExtension;
    string indexFilename = "Generated/" + ResourceName + IndexExtension;
    if (File.Exists(fileFilename) && File.Exists(indexFilename))
    {
      using (var indexStream = new FileStream(indexFilename, FileMode.Open))
      {
        indexStream.Position = indexStream.Length - 4;
        byte[] countBytes = new byte[4];
        indexStream.ReadExactly(countBytes, 0, countBytes.Length);
        indexStream.Position = 0;
      
        int count = BitConverter.ToInt32(countBytes, 0);
        for (int i = 0; i < count; i++)
        {
          byte[] offsetBytes = new byte[8];
          indexStream.ReadExactly(offsetBytes, 0, offsetBytes.Length);
          table.Add(BitConverter.ToInt64(offsetBytes));
        }
      }
      _fileStream = new FileStream(fileFilename, FileMode.Open, FileAccess.Read);
    } 
    else context.Managers.Debug.Warning("No resource files / index file found. Ignore this warning if you dont plan on using external resources (such as textures, sounds, fonts and so on).");
    
    #endif
    _offsetTable = table.ToArray();
  }
  
  #if RESOURCES
  public void Pack(Dictionary<string, Dictionary<string, string>> resourceFiles)
  {
    var folderPath = Path.GetDirectoryName(ExportGeneratedPath);
    if (!Directory.Exists(folderPath)) 
      Directory.CreateDirectory(folderPath!);

    using (var fileStream = new FileStream(ExportGeneratedPath + ResourceName + OutputExtension, FileMode.Create))
    using (var indexStream = new FileStream(ExportGeneratedPath + ResourceName + IndexExtension, FileMode.Create))
    using (var structWriter = new StreamWriter(ExportGeneratedPath + StructName + ".cs"))
    {
      structWriter.WriteLine("#if !RESOURCES\nnamespace StreamlineEngine.Generated;");
      structWriter.WriteLine($"public struct {Path.GetFileName(StructName)}");
      structWriter.WriteLine("{");
      
      Span<byte> header = stackalloc byte[8];
      
      foreach (var resourceFile in resourceFiles)
      {
        string packagingMethod = resourceFile.Value["type"];
        
        ResourceType resourceType;
        byte[] resourceData;
        
        switch (packagingMethod)
        {
          case "single":
            EncodeResource(header, fileStream, indexStream, resourceFile, "", out resourceType, out resourceData);
            structWriter.WriteLine($"  public const int {resourceFile.Key} = {_resourceId-1};   // {resourceType}");
            
            Console.WriteLine($"Packed '{resourceFile.Key}', Filename: {resourceFile.Value["path"]}, Type: {resourceType}, Size: ~{resourceData.Length / 1024}KB, ID: {_resourceId-1}");
            break;
          case "stack":
            string[] files = Directory.GetFiles(Config.ResourcesPath + resourceFile.Value["path"]).OrderBy(file => int.Parse(Regex.Match(file, @"\d+").Value)).ToArray();
            int startId = _resourceId;
            List<ResourceType> resourceTypes = [];
            foreach (string file in files)
            {
              string filename = Path.GetFileName(file);
              EncodeResource(header, fileStream, indexStream, resourceFile, filename, out resourceType, out resourceData);
              resourceTypes.Add(resourceType);
              
              Console.WriteLine($"Packed '{resourceFile.Key}', Filename: {resourceFile.Value["path"] + filename}, Type: {resourceType}, Size: ~{resourceData.Length / 1024}KB, ID: {_resourceId-1}");
            }
            if (resourceTypes.Any(r => r != resourceTypes[0])) throw new InvalidOperationException("All resources must be of the same type");
            structWriter.WriteLine($"  public static readonly Range {resourceFile.Key} = {startId}..{_resourceId-1};   // {resourceTypes[0]}");
            break;
        }
      }
      structWriter.WriteLine("}\n#endif");
      indexStream.Write(BitConverter.GetBytes(_resourceId));
    }
  }

  private void EncodeResource(Span<byte> header, FileStream fileStream, FileStream indexStream, KeyValuePair<string, Dictionary<string, string>> resourceFile, string addPath, out ResourceType resourceType, out byte[] resourceData)
  {
    resourceType = ResourceMap[Path.GetExtension(resourceFile.Value["path"] + addPath).ToLower()];
    resourceData = File.ReadAllBytes(Config.ResourcesPath + resourceFile.Value["path"] + addPath);

    BitConverter.TryWriteBytes(header[..4], resourceData.Length);
    BitConverter.TryWriteBytes(header[4..], (int)resourceType);
    fileStream.Write(header);
    fileStream.Write(resourceData);
        
    byte[] offsetBytes = BitConverter.GetBytes(_lastOffset);
    indexStream.Write(offsetBytes, 0, offsetBytes.Length);
    _lastOffset += resourceData.Length + 8;
    _resourceId++;
  }
  #endif
  
  public T Unpack<T>(int resourceID)
  {
    string resourcesFilename = "Generated/" + ResourceName + OutputExtension;
    if (!File.Exists(resourcesFilename))
      throw new FileNotFoundException();
    
    if (resourceID >= _offsetTable.Length)
      throw new IndexOutOfRangeException("Resource ID is out of range");
    
    _fileStream.Seek(_offsetTable[resourceID], SeekOrigin.Begin);
    
    Span<byte> headerBuffer = stackalloc byte[8];
    
    _fileStream.ReadExactly(headerBuffer);
    int sizeLength = BitConverter.ToInt32(headerBuffer[..4]);
    ResourceType resourceType = (ResourceType)BitConverter.ToInt32(headerBuffer[4..]);
      
    byte[] resourceData = new byte[sizeLength];
    _fileStream.ReadExactly(resourceData, 0, sizeLength);
    return LoadResourceByType<T>(resourceData, resourceType);
  }
  
  public T[] UnpackMany<T>(int[] resourceIDs)
  {
    string resourcesFilename = "Generated/" + ResourceName + OutputExtension;
    if (!File.Exists(resourcesFilename))
      throw new FileNotFoundException();

    if (resourceIDs.Any(id => id >= _offsetTable.Length))
      throw new IndexOutOfRangeException("At least one of Resource IDs is out of range");
    
    Array.Sort(resourceIDs);

    List<T> resources = [];
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
    if (typeof(T) == typeof(Font)) 
      return (T)(object)Raylib.LoadFontFromMemory(ext, resourceData, (int)Defaults.FontSize, [], 0);
    
    throw new InvalidOperationException("Invalid resource type");
  }
}