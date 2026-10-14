namespace P01;

public enum PracticeTask
{
  Ex1,
  Ex2,
  Ex3,
}

public enum Member
{
  Janne,
  RichardB,
  OlaS,
}

internal static class Program
{
  internal static void Main()
  {
    PracticeTask userSelect = PracticeTask.Ex1;

    switch (userSelect)
    {
      case PracticeTask.Ex1:
        //träna på nåt här
        /* Intro: abstrakt basklass + override
Skapa den abstrakta basklassen Animal med en abstrakt metod MakeSound().
Skapa två subklasser Dog och Cat som ärver från Animal.
Implementera MakeSound() i båda subklasserna så att Dog returnerar "Woof!" och Cat returnerar "Meow!".
I Main-metoden, skapa en lista av Animal-objekt som innehåller både Dog och Cat.
Loop genom listan och anropa MakeSound() för varje djur, och skriv ut resultatet till konsolen.
Bonus: Lägg till propertyn Name i Animal-klassen och sätt namn för varje djur. Skriv ut namnet tillsammans med ljudet. */

        List<Animal> animals =
        [
          new Dog(Member.Janne),
          new Cat(Member.RichardB),
          new Cat(Member.OlaS),
        ];

        foreach (Animal animal in animals)
        {
          Present.PrintToConsole(Present.BuildMessage(animal));
        }

        break;

      case PracticeTask.Ex2:
        //träna på något annat här
        break;
    }
  }
}

internal class Cat(Member name) : Animal(name)
{
  private readonly string _sound = "Meow!";

  public override string MakeSound()
  {
    return _sound;
  }

  public override string GetTypeAsString()
  {
    return "Katt";
  }
}

internal abstract class Animal(Member name)
{
  public Member Name { get; private set; } = name;
  public abstract string MakeSound();
  public abstract string GetTypeAsString();
}

internal class Dog(Member name) : Animal(name)
{
  private readonly string _sound = "Woof!";

  public override string MakeSound()
  {
    return _sound;
  }

  public override string GetTypeAsString()
  {
    return "Hund";
  }
}

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
