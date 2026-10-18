namespace P01.src.Infrastructure;

internal static class DataLayer
{
  internal static void SaveToFile(string filePath, string content)
  {
    File.AppendAllText($"{filePath}.txt", content);
  }
}
