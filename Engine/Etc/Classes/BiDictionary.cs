namespace StreamlineEngine.Engine.Etc.Classes;

public class BiDictionary<T1, T2> : IEnumerable<KeyValuePair<T1, T2>> where T1 : notnull where T2 : notnull
{
  public readonly Dictionary<T1, T2> Forward = new();
  public readonly Dictionary<T2, T1> Reverse = new();

  public BiDictionary() { }

  public BiDictionary(IEnumerable<KeyValuePair<T1, T2>> pairs)
  {
    foreach (var pair in pairs)
    {
      Add(pair.Key, pair.Value);
    }
  }

  public void Add(T1 key, T2 value)
  {
    Forward[key] = value;
    Reverse[value] = key;
  }
  
  public T2 this[T1 key] => Forward.TryGetValue(key, out var value) 
    ? value 
    : throw new KeyNotFoundException($"Key '{key}' not found.");
  
  public T1 this[T2 value] => Reverse.TryGetValue(value, out var key)
    ? key
    : throw new KeyNotFoundException($"Value '{value}' not found.");

  public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator() => Forward.GetEnumerator();
  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
