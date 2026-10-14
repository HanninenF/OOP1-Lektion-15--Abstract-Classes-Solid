namespace P01;

public enum PracticeTask
{
  Ex1,
  Ex2,
  Ex3,
}

internal static class Program
{
  internal static void Main()
  {
    PracticeTask userSelect = PracticeTask.Ex2;

    switch (userSelect)
    {
      case PracticeTask.Ex1:
        //träna på nåt här
        break;

      case PracticeTask.Ex2:
        //träna på något annat här
        break;
    }
  }
}
