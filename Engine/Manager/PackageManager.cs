using System.Text.Json;
using Raylib_cs;
using StreamlineEngine.Engine.Etc.Classes;

namespace StreamlineEngine.Engine.Manager;


public enum ResourceType
{
  ImagePng,
  ImageJpg,
  SoundMp3,
}

public class PackageManager(string outputFilename, string enumFilename, string enumGeneratedExportPath = "../../../")
{
  private static readonly BiDictionary<string, ResourceType> ResourceMap = new()
  {
    { ".png", ResourceType.ImagePng },
    { ".jpg", ResourceType.ImageJpg },
    { ".mp3", ResourceType.SoundMp3 },
  };
  const string OutputExtension = ".serp";
  
  public void Pack(Dictionary<string, string> resourceFiles)
  {
    var folderPath = Path.GetDirectoryName(enumGeneratedExportPath);
    if (!Directory.Exists(folderPath)) 
      Directory.CreateDirectory(folderPath!);
    using (var fileStream = new FileStream(enumGeneratedExportPath + outputFilename + OutputExtension, FileMode.Create))
    using (var enumWriter = new StreamWriter(enumGeneratedExportPath + enumFilename + ".cs"))
    {
      enumWriter.WriteLine("namespace StreamlineEngine.Generated;");
      enumWriter.WriteLine($"public enum {Path.GetFileName(enumFilename)}");
      enumWriter.WriteLine("{");
      
      int resourceId = 0;
      int maxSpace = resourceFiles.Keys.Max(k => k.Length) + 2;

      foreach (var resourceFile in resourceFiles)
      {
        byte[] resourceData = File.ReadAllBytes(Config.ResourcesPath + resourceFile.Value);
        ResourceType resourceType = ResourceMap[Path.GetExtension(resourceFile.Value).ToLower()];
        
        byte[] idBytes = BitConverter.GetBytes(resourceId);
        fileStream.Write(idBytes, 0, idBytes.Length);
        
        byte[] sizeBytes = BitConverter.GetBytes(resourceData.Length);
        fileStream.Write(sizeBytes, 0, sizeBytes.Length);
        
        byte[] typeBytes = BitConverter.GetBytes((int)resourceType);
        fileStream.Write(typeBytes, 0, typeBytes.Length);

        fileStream.Write(resourceData, 0, resourceData.Length);

        enumWriter.WriteLine($"  {resourceFile.Key} = {resourceId},{new string(' ', maxSpace - resourceFile.Key.Length)}// {ResourceMap[resourceType]}");
        string left = $"Packed resource '{resourceFile.Key}',     Type: '{resourceType}',     Size: '~{resourceData.Length/1024}KB'     ID: {resourceId}";
        string right = $"{resourceId + 1}/{resourceFiles.Count}";
        string rightSpace = new string(' ', Console.WindowWidth - left.Length - right.Length);
        Console.WriteLine(left + rightSpace + right);
        resourceId++;
      }

      enumWriter.WriteLine("}");
    }
  }
  
  public T Unpack<T>(int resourceID)
  {
    if (!Raylib.IsWindowReady()) throw new InvalidOperationException("Unpacking should be called after window initialization!");

    string resourcesFilename = "Generated/" + outputFilename + OutputExtension;
    if (!File.Exists(resourcesFilename)) throw new FileNotFoundException();
  
    using (var fileStream = new FileStream(resourcesFilename, FileMode.Open))
    {
      byte[] idBytes = new byte[4];
      byte[] sizeBytes = new byte[4];
      byte[] typeBytes = new byte[4];
      while (fileStream.Read(idBytes, 0, 4) > 0)
      {
        int currentResourceID = BitConverter.ToInt32(idBytes, 0);
      
        fileStream.ReadExactly(sizeBytes, 0, 4);
        int sizeLength = BitConverter.ToInt32(sizeBytes, 0);
      
        if (currentResourceID == resourceID)
        {
          fileStream.ReadExactly(typeBytes, 0, 4);
          ResourceType resourceType = (ResourceType)BitConverter.ToInt32(typeBytes, 0);
        
          byte[] resourceData = new byte[sizeLength];
          fileStream.ReadExactly(resourceData, 0, sizeLength);

          return LoadResourceByType<T>(resourceData, resourceType);
        }
        fileStream.Seek(4 + sizeLength, SeekOrigin.Current);
      }
    }
    throw new InvalidOperationException("Resource not found");
  }
  
  public T[] UnpackMany<T>(int[] resourceIDs)
  {
    T[] resources = new T[resourceIDs.Length];
    for (int i = 0; i < resourceIDs.Length; i++)
      resources[i] = Unpack<T>(resourceIDs[i]);
    return resources;
  }

  public static Dictionary<string, string> GetJsonToPackAsDict(string filename)
  {
    string json = File.ReadAllText(filename);
    Dictionary<string, string>? dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    if (dict == null) throw new NullReferenceException("Resources JSON is empty!");
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