namespace P01.src.Infrastructure;

internal static class DataLayer //ska det här in i infra?
{
  internal static void SaveToFile(string filePath, string content)
  {
    File.AppendAllText($"{filePath}.txt", content);
  }
}
