using P01.src.Domain;

namespace P01.src.Presentation;

internal static class Present
{
  public static void PrintToConsole(string message)
  {
    Console.WriteLine(message);
  }

  private static string FormatMemberName(Member member)
  {
    string name = member.ToString();

    if (name.Length > 1 && char.IsUpper(name[^1]))
    {
      name = name.Insert(name.Length - 1, ".");
    }

    return name;
  }

  public static string BuildMessage(Animal animal)
  {
    string nameToPrint = FormatMemberName(animal.Name);
    string suffix = "en";
    string animalTypeToPrint = animal.GetTypeAsString() + suffix;

    string message = $"{animalTypeToPrint} heter {nameToPrint} och säger {animal.MakeSound()}";

    return message;
  }
}
