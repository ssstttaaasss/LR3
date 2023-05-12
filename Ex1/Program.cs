using System;
using System.IO;
using Newtonsoft.Json;

class DecimalCounter
{
    private int _value;
    private int _minValue;
    private int _maxValue;

    public DecimalCounter()
    {
        _value = 0;
        _minValue = 0;
        _maxValue = 10;
    }

    public DecimalCounter(int initialValue, int minValue, int maxValue)
    {
        if (initialValue < minValue || initialValue > maxValue)
        {
            throw new ArgumentException("Початкове значення має бути в межах діапазону.");
        }

        _value = initialValue;
        _minValue = minValue;
        _maxValue = maxValue;
    }

    public int GetValue()
    {
        return _value;
    }

    public void Increment()
    {
        if (_value < _maxValue)
        {
            _value++;
        }
    }

    public void Decrement()
    {
        if (_value > _minValue)
        {
            _value--;
        }
    }
    
    string filePath = "/Users/stanislavpodolaka/Desktop/КПІ/прога/workspace/myCounter.json";

    
    public void SerializeToJson(string filePath)
    {
        string json = JsonConvert.SerializeObject(this);
        File.WriteAllText(filePath, json);
    }

    public static DecimalCounter DeserializeFromJson(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<DecimalCounter>(json);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створення екземпляру зі значеннями за замовчуванням
        DecimalCounter counter1 = new DecimalCounter();

        // Створення екземпляру з довільними значеннями
        DecimalCounter counter2 = new DecimalCounter(5, 0, 7);

        // Перевірка метода лічильника
        Console.WriteLine("Counter 1 initial value: " + counter1.GetValue());
        Console.WriteLine("Counter 2 initial value: " + counter2.GetValue());

        counter1.Increment();
        counter2.Decrement();

        Console.WriteLine("Counter 1 incremented value: " + counter1.GetValue());
        Console.WriteLine("Counter 2 decremented value: " + counter2.GetValue());

        // Тестова серіалізація та десеріалізація
        counter1.SerializeToJson("counter1.json");
        DecimalCounter deserializedCounter = DecimalCounter.DeserializeFromJson("counter1.json");
        Console.WriteLine("Deserialized counter value: " + deserializedCounter.GetValue());
    }
}
