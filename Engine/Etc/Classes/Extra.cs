namespace StreamlineEngine.Engine.Etc.Classes;

public static class Extra
{
  /// <summary>
  /// Converts enum to range (including both sides)
  /// </summary>
  public static Range EnumToRange<TEnum>(TEnum start, TEnum end) where TEnum : Enum =>
    (int)(object)start..(int)(object)end;
  
  
  /// <summary>
  /// Converts range to array (including both sides)
  /// </summary>
  /// <param name="range">Range to convert</param>
  /// <returns>int[]</returns>
  public static int[] RangeToArray(Range range) => 
    Enumerable.Range(range.Start.Value, range.End.Value - range.Start.Value + 1).ToArray();
}