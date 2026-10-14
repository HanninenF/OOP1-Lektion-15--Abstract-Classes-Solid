namespace P01.src.Domain;

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
