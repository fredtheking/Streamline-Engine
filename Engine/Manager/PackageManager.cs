using System.Text.Json;
using Raylib_cs;

namespace StreamlineEngine.Engine.Manager;

public class PackageManager(string outputFilename, string enumFilename, string enumGeneratedExportPath = "../../../")
{
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

      foreach (var resourceFile in resourceFiles)
      {
        byte[] resourceData = File.ReadAllBytes(Config.ResourcesPath + resourceFile.Value);
        ResourceType resourceType = GetResourceType(Config.ResourcesPath + resourceFile.Value);

        byte[] idBytes = BitConverter.GetBytes(resourceId);
        fileStream.Write(idBytes, 0, idBytes.Length);
        
        byte[] sizeBytes = BitConverter.GetBytes(resourceData.Length);
        fileStream.Write(sizeBytes, 0, sizeBytes.Length);
        
        byte[] typeBytes = BitConverter.GetBytes((int)resourceType);
        fileStream.Write(typeBytes, 0, typeBytes.Length);

        fileStream.Write(resourceData, 0, resourceData.Length);

        enumWriter.WriteLine($"  {resourceFile.Key} = {resourceId},");
        string left = $"Packed resource '{resourceFile.Key}',     Type: '{resourceType}',     ID: {resourceId}";
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

  public static Dictionary<string, string> GetJsonToPackAsDict(string filename)
  {
    string json = File.ReadAllText(filename);
    Dictionary<string, string>? dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    if (dict == null) throw new NullReferenceException("Resources JSON is empty!");
    return dict;
  }
  
  private T LoadResourceByType<T>(byte[] resourceData, ResourceType resourceType)
  {
    string ext = GetExtensionByResourceType(resourceType);
    if (typeof(T) == typeof(Texture2D))
    {
      Image image = Raylib.LoadImageFromMemory(ext, resourceData);
      return (T)(object)Raylib.LoadTextureFromImage(image);
    }
    if (typeof(T) == typeof(Image))
    {
      Image image = Raylib.LoadImageFromMemory(ext, resourceData);
      return (T)(object)image;
    }
    if (typeof(T) == typeof(Wave))
    {
      Wave wave = Raylib.LoadWaveFromMemory(ext, resourceData);
      return (T)(object)wave;
    }
    if (typeof(T) == typeof(Sound))
    {
      Wave wave = Raylib.LoadWaveFromMemory(ext, resourceData);
      return (T)(object)Raylib.LoadSoundFromWave(wave);
    }
    throw new InvalidOperationException("Invalid resource type");
  }

  private ResourceType GetResourceType(string fileName)
  {
    return Path.GetExtension(fileName).ToLower() switch
    {
      ".png" => ResourceType.ImagePng,
      ".jpg" => ResourceType.ImageJpg,
      ".mp3" => ResourceType.WaveMp3,
      _ => throw new InvalidOperationException("Invalid file extension")
    };
  }
  
  private string GetExtensionByResourceType(ResourceType resourceType)
  {
    return resourceType switch
    {
      ResourceType.ImagePng => ".png",
      ResourceType.ImageJpg => ".jpg",
      ResourceType.WaveMp3 => ".mp3",
      _ => throw new InvalidOperationException("Invalid resource type")
    };
  }
}

public enum ResourceType
{
  ImagePng,
  ImageJpg,
  WaveMp3,
}