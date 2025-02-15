namespace StreamlineEngine.Engine.Etc.Classes;

public class RefObj<T>(T value)
{
  public T Value { get; set; } = value;
}