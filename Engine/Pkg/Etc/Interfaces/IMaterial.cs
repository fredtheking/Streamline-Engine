using Raylib_cs;

namespace StreamlineEngine.Engine.Pkg.Etc.Interfaces;

public interface IMaterial<TFilename, TMaterial> : IScript
{
  public TFilename Filename { get; set; }
  public TMaterial Material { get; set; }
}