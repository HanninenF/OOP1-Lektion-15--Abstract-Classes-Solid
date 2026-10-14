using P01.src.Domain;
using P01.src.Presentation;

namespace P01.src;

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
