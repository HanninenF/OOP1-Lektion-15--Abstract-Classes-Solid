using P01.src.Domain;
using P01.src.Infrastructure;
using P01.src.Presentation;

namespace P01.src;

internal static class Program
{
  internal static void Main()
  {
    PracticeTask userSelect = PracticeTask.SolidExercises;

    switch (userSelect)
    {
      case PracticeTask.UsingAbstractClassesAndMethodOverridesForPolymorphism:
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
        /* 1. Abstrakt klass för Sensorer
Vi låtsas att vi gör ett system för att hantera olika typer av sensorer i ett smart hem.

Skapa en abstrakt basklass Sensor som definierar gemensamma egenskaper och metoder för alla sensorer.

Lägg till en abstrakt metod ReadValue() som ska returnera ett värde från sensorn av typ double.

Lägg till en property Unit som beskriver enhet för sensorn (t.ex. "Celsius", "Percentage", etc.).

Skapa två subklasser TemperatureSensor och HumiditySensor som ärver från Sensor.

ReadValue() i TemperatureSensor ska returnera ett slumpmässigt temperaturvärde mellan -10 och 50 grader Celsius,

ReadValue() i HumiditySensor ska returnera ett slumpmässigt fuktighetsvärde mellan 0 och 100 procent.

I Main-metoden, skapa en lista av Sensor-objekt som innehåller både TemperatureSensor och HumiditySensor.

Loop genom listan och anropa ReadValue() för varje sensor, och skriv ut resultatet till konsolen.

Lägg till ytterligare en subklass LightSensor som också ärver från Sensor.

ReadValue() i LightSensor ska returnera ett slumpmässigt ljusvärde mellan 0 och 1000 lux.

Lägg till LightSensor i listan av sensorer i Main-metoden och kör programmet. Se att alla sensorer fungerar korrekt.

 */

        List<Sensor> sensors =
        [
          new TemperatureSensor(Unit.Celcius),
          new HumiditySensor(Unit.Percentage),
          new LightSensor(Unit.Lux),
        ];

        foreach (Sensor sensor in sensors)
        {
          string message = $"{sensor.ReadValue()}{sensor.Unit}\n";
          Present.PrintToConsole(message);
          DataLayer.SaveToFile("SensorInfo", message);
        }
        break;
      case PracticeTask.SolidExercises:
        // Övning 1 – SRP: Order ansvarar för beräkningar och ReceiptPrinter för presentation.
        var items = new List<OrderItem> { new("Bok", 2, 120m), new("Penna", 5, 10m) };

        // Övning 2 – OCP: Nya rabattregler ska kunna läggas till utan att Order ändras.
        DiscountPolicy discountPolicy = new ThresholdDiscount(300, 0.1m);
        DiscountPolicy itemCountDiscount = new ItemCountDiscount();
        var orderWithItemCountDiscount = new Order(itemCountDiscount);
        orderWithItemCountDiscount.Add(items);

        var order = new Order(discountPolicy);
        order.Add(items);

        DiscountPolicy paltkomaDiscount = new PaltkomaDiscount();
        Order orderPaltkomaDiscount = new(paltkomaDiscount);
        orderPaltkomaDiscount.Add(items);

        ReceiptPrinter.PrintReceipt(order); // Skriver ut totalsumma + rader
        ReceiptPrinter.PrintReceipt(orderWithItemCountDiscount);
        ReceiptPrinter.PrintReceipt(orderPaltkomaDiscount);

        break;

      default:
        break;
    }
  }
}

class OrderItem
{
  public string Name { get; }
  public int Quantity { get; }
  public decimal UnitPrice { get; }

  public OrderItem(string name, int qty, decimal price) =>
    (Name, Quantity, UnitPrice) = (name, qty, price);
}

class Order(DiscountPolicy discountPolicy)
{
  private readonly List<OrderItem> _items = new();

  public List<OrderItem> GetOrderItems()
  {
    return _items;
  }

  public int GetTotalItems()
  {
    int totalItems = 0;
    for (int i = 0; i < _items.Count; i++)
    {
      totalItems += _items[i].Quantity;
    }
    return totalItems;
  }

  public void Add(IEnumerable<OrderItem> items) => _items.AddRange(items);

  // ANSVAR 1: beräkningar
  public decimal CalculateSubtotal()
  {
    decimal sum = 0;
    foreach (var i in _items)
      sum += i.UnitPrice * i.Quantity;
    return sum;
  }

  public decimal CalculateVat(decimal rate = 0.25m)
  {
    return CalculateSubtotal() * rate;
  }

  public decimal CalculateTotal(decimal rate = 0.25m)
  {
    DiscountContext discountContext = new(
      CalculateSubtotal() + CalculateVat(rate),
      GetTotalItems()
    );
    decimal discount = discountPolicy.CalculateDiscount(discountContext);
    return CalculateSubtotal() + CalculateVat(rate) - discount;
  }
}

// OCP: Nya rabattregler kan läggas till som nya subklasser utan att Order ändras.
internal abstract class DiscountPolicy()
{
  public abstract decimal CalculateDiscount(DiscountContext discountContext);
}

internal class ThresholdDiscount(decimal threshold, decimal discount) : DiscountPolicy
{
  public override decimal CalculateDiscount(DiscountContext discountContext)
  {
    if (discountContext.Sum > threshold)
    {
      return discountContext.Sum * discount;
    }
    else
      return 0;
  }
}

internal class ItemCountDiscount : DiscountPolicy
{
  public override decimal CalculateDiscount(DiscountContext discountContext)
  {
    decimal discount = 0.1m;
    if (discountContext.ItemCount > 10)
    {
      return discountContext.Sum * discount;
    }
    else
      return 0;
  }
}

internal class PaltkomaDiscount : DiscountPolicy
{
  public override decimal CalculateDiscount(DiscountContext discountContext)
  {
    DateTimeOffset now = DateTimeOffset.Now;

    decimal discount = 0.1m;
    if (now.Hour > 12 && now.Hour < 21)
    {
      return discountContext.Sum * discount;
    }
    else
      return 0;
  }
}

internal class DiscountContext(decimal sum, int itemCount)
{
  public decimal Sum { get; private set; } = sum;
  public int ItemCount { get; private set; } = itemCount;
}

internal static class ReceiptPrinter
{
  public static void PrintReceipt(Order order)
  {
    Console.WriteLine("KVITTO");
    foreach (var i in order.GetOrderItems())
    {
      Console.WriteLine($"{i.Name} x{i.Quantity} á {i.UnitPrice:C} = {i.UnitPrice * i.Quantity:C}");
    }
    Console.WriteLine($"Delsumma: {order.CalculateSubtotal():C}");
    Console.WriteLine($"Moms (25%): {order.CalculateVat():C}");
    Console.WriteLine($"Totalt: {order.CalculateTotal():C}");
  }
}
