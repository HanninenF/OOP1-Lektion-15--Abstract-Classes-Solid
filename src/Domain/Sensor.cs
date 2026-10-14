namespace P01.src.Domain;

internal abstract class Sensor(string unit)
{
  public string Unit { get; private set; } = unit;
  public abstract double ReadValue();
}

internal class TemperatureSensor(string unit) : Sensor(unit)
{
  public override double ReadValue()
  {
    return Random.Shared.Next(-10, 50);
  }
}

internal class HumiditySensor(string unit) : Sensor(unit)
{
  public override double ReadValue()
  {
    return Random.Shared.Next(0, 100);
  }
}

internal class LightSensor(string unit) : Sensor(unit)
{
  public override double ReadValue()
  {
    return Random.Shared.Next(0, 1000);
  }
}
